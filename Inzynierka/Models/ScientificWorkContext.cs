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

	    public virtual DbSet<ActivityPlan> ActivityPlans
	    {
	        get; set;
	    } 
        public virtual DbSet<WorkPlan> WorkPlans
        {
            get; set;
        }

		protected override void OnModelCreating( DbModelBuilder modelBuilder )
		{
			
		}
	}
}
