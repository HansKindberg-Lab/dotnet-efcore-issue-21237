using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
	[TestClass]
	public class Test
	{
		#region Fields

		private const string _dataDirectoryName = "DataDirectory";
		private static readonly string _projectDirectoryPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
		private static readonly string _dataDirectoryPath = Path.Combine(_projectDirectoryPath, "Data");

		#endregion

		#region Methods

		[AssemblyInitialize]
		public static void Initialize(TestContext testContext)
		{
			AppDomain.CurrentDomain.SetData(_dataDirectoryName, _dataDirectoryPath);
		}

		[TestMethod]
		public void Sqlite_EnsureCreated_Test1()
		{
			var databaseFilePath = Path.Combine(_dataDirectoryPath, "SQLite-3.db");
			Assert.IsFalse(File.Exists(databaseFilePath));

			var services = new ServiceCollection();
			services.AddDbContext<DatabaseContext>(builder => builder.UseSqlite("Data Source=|DataDirectory|SQLite-3.db"));
			using(var scope = services.BuildServiceProvider().CreateScope())
			{
				var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				databaseContext.Database.EnsureCreated();
				Assert.IsTrue(File.Exists(databaseFilePath));
				databaseContext.Database.EnsureDeleted();
			}
		}

		[TestMethod]
		public void Sqlite_EnsureCreated_Test2()
		{
			var databaseFilePath = Path.Combine(_dataDirectoryPath, "SQLite-4.db");
			Assert.IsFalse(File.Exists(databaseFilePath));

			var services = new ServiceCollection();
			services.AddDbContext<DatabaseContext>(builder => builder.UseSqlite($"Data Source={_dataDirectoryPath}\\SQLite-4.db"));
			using(var scope = services.BuildServiceProvider().CreateScope())
			{
				var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				databaseContext.Database.EnsureCreated();
				Assert.IsTrue(File.Exists(databaseFilePath));
				databaseContext.Database.EnsureDeleted();
			}
		}

		[TestMethod]
		public void Sqlite_Migrate_Test1()
		{
			var databaseFilePath = Path.Combine(_dataDirectoryPath, "SQLite-1.db");
			Assert.IsFalse(File.Exists(databaseFilePath));

			var services = new ServiceCollection();
			services.AddDbContext<DatabaseContext>(builder => builder.UseSqlite("Data Source=|DataDirectory|SQLite-1.db"));
			using(var scope = services.BuildServiceProvider().CreateScope())
			{
				var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				databaseContext.Database.Migrate();
				Assert.IsTrue(File.Exists(databaseFilePath));
				databaseContext.Database.EnsureDeleted();
			}
		}

		[TestMethod]
		public void Sqlite_Migrate_Test2()
		{
			var databaseFilePath = Path.Combine(_dataDirectoryPath, "SQLite-2.db");
			Assert.IsFalse(File.Exists(databaseFilePath));

			var services = new ServiceCollection();
			services.AddDbContext<DatabaseContext>(builder => builder.UseSqlite($"Data Source={_dataDirectoryPath}\\SQLite-2.db"));
			using(var scope = services.BuildServiceProvider().CreateScope())
			{
				var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				databaseContext.Database.Migrate();
				Assert.IsTrue(File.Exists(databaseFilePath));
				databaseContext.Database.EnsureDeleted();
			}
		}

		[TestMethod]
		public void SqlServer_LocalDB_EnsureCreated_Test1()
		{
			var databaseFilePath = Path.Combine(_dataDirectoryPath, "LocalDB-3.mdf");
			Assert.IsFalse(File.Exists(databaseFilePath));

			var services = new ServiceCollection();
			services.AddDbContext<DatabaseContext>(builder => builder.UseSqlServer($"Server=(LocalDB)\\MSSQLLocalDB;AttachDbFileName=|DataDirectory|LocalDB-3.mdf;Database={databaseFilePath};Integrated Security=True"));
			using(var scope = services.BuildServiceProvider().CreateScope())
			{
				var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				databaseContext.Database.EnsureCreated();
				Assert.IsTrue(File.Exists(databaseFilePath));
				databaseContext.Database.EnsureDeleted();
			}
		}

		[TestMethod]
		public void SqlServer_LocalDB_EnsureCreated_Test2()
		{
			var databaseFilePath = Path.Combine(_dataDirectoryPath, "LocalDB-4.mdf");
			Assert.IsFalse(File.Exists(databaseFilePath));

			var services = new ServiceCollection();
			services.AddDbContext<DatabaseContext>(builder => builder.UseSqlServer($"Server=(LocalDB)\\MSSQLLocalDB;AttachDbFileName={_dataDirectoryPath}\\LocalDB-4.mdf;Initial Catalog={databaseFilePath};Integrated Security=True"));
			using(var scope = services.BuildServiceProvider().CreateScope())
			{
				var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				databaseContext.Database.EnsureCreated();
				Assert.IsTrue(File.Exists(databaseFilePath));
				databaseContext.Database.EnsureDeleted();
			}
		}

		[TestMethod]
		public void SqlServer_LocalDB_Migrate_Test1()
		{
			var databaseFilePath = Path.Combine(_dataDirectoryPath, "LocalDB-1.mdf");
			Assert.IsFalse(File.Exists(databaseFilePath));

			var services = new ServiceCollection();
			services.AddDbContext<DatabaseContext>(builder => builder.UseSqlServer($"Server=(LocalDB)\\MSSQLLocalDB;AttachDbFileName=|DataDirectory|LocalDB-1.mdf;Database={databaseFilePath};Integrated Security=True"));
			using(var scope = services.BuildServiceProvider().CreateScope())
			{
				var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				databaseContext.Database.Migrate();
				Assert.IsTrue(File.Exists(databaseFilePath));
				databaseContext.Database.EnsureDeleted();
			}
		}

		[TestMethod]
		public void SqlServer_LocalDB_Migrate_Test2()
		{
			var databaseFilePath = Path.Combine(_dataDirectoryPath, "LocalDB-2.mdf");
			Assert.IsFalse(File.Exists(databaseFilePath));

			var services = new ServiceCollection();
			services.AddDbContext<DatabaseContext>(builder => builder.UseSqlServer($"Server=(LocalDB)\\MSSQLLocalDB;AttachDbFileName={_dataDirectoryPath}\\LocalDB-2.mdf;Initial Catalog={databaseFilePath};Integrated Security=True"));
			using(var scope = services.BuildServiceProvider().CreateScope())
			{
				var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				databaseContext.Database.Migrate();
				Assert.IsTrue(File.Exists(databaseFilePath));
				databaseContext.Database.EnsureDeleted();
			}
		}

		#endregion
	}
}