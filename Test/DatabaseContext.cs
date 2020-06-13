using Microsoft.EntityFrameworkCore;

namespace Test
{
	public class DatabaseContext : DbContext
	{
		#region Constructors

		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

		#endregion
	}
}