using ARM_Vyz.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM_Vyz.Model
{
	public class UniversityEntities : DbContext
	{
		public UniversityEntities() : base("name=InventoryManagmentEntities")
		{
			Database.CreateIfNotExists();
		}

		public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
		public virtual DbSet<Departments> Departments { get; set; }
		public virtual DbSet<Faculties> Faculties { get; set; }
		public virtual DbSet<People> People { get; set; }
		public virtual DbSet<Roles> Roles { get; set; }
		public virtual DbSet<ScientificDegrees> ScientificDegrees { get; set; }
		public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Departments>()
				.Property(e => e.DepartmentName)
				.IsFixedLength();

			modelBuilder.Entity<Departments>()
				.HasMany(e => e.People)
				.WithRequired(e => e.Departments)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Faculties>()
				.Property(e => e.FacultyName)
				.IsFixedLength();

			modelBuilder.Entity<People>()
				.Property(e => e.FIO)
				.IsFixedLength();

			modelBuilder.Entity<People>()
				.Property(e => e.Scholarship)
				.HasPrecision(19, 4);

			modelBuilder.Entity<People>()
				.Property(e => e.Gender)
				.IsFixedLength();

			modelBuilder.Entity<People>()
				.Property(e => e.Salary)
				.HasPrecision(19, 4);

			modelBuilder.Entity<People>()
				.HasMany(e => e.Faculties)
				.WithOptional(e => e.People)
				.HasForeignKey(e => e.DeanID);

			modelBuilder.Entity<People>()
				.HasMany(e => e.ScientificDegrees)
				.WithOptional(e => e.People)
				.HasForeignKey(e => e.TeacherID);

			modelBuilder.Entity<Roles>()
				.Property(e => e.RoleName)
				.IsFixedLength();

			modelBuilder.Entity<ScientificDegrees>()
				.Property(e => e.NameDissertation)
				.IsFixedLength();
		}
	}
}
