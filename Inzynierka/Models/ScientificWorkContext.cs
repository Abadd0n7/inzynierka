namespace Inzynierka.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class ScientificWorkContext : DbContext
	{
		public ScientificWorkContext()
			: base( "name=ScientificWorkContext" )
		{
		}

		public virtual DbSet<SampleTable> SampleTables
		{
			get; set;
		}

		protected override void OnModelCreating( DbModelBuilder modelBuilder )
		{
			modelBuilder.Entity<SampleTable>()
				.Property( e => e.StrangeVal )
				.IsFixedLength();
		}
	}
}
