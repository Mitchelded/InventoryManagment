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
			this.Configuration.LazyLoadingEnabled = true;
			if(Database.CreateIfNotExists())
			{
				Faculties.Add(new Entities.Faculties { FacultyName = "FacultyName" });
				SaveChanges();
				Departments.Add(new Entities.Departments {DepartmentName= "DepartmentName", FacultyID = 1 });
				SaveChanges();
				Roles.AddRange(new Roles[]
				{
					new Entities.Roles { RoleName = "Teacher"},
					new Entities.Roles { RoleName = "Dean"},
				});
				SaveChanges();
			}
		}

		public virtual DbSet<Departments> Departments { get; set; }
		public virtual DbSet<Dessertations> Dessertations { get; set; }
		public virtual DbSet<Faculties> Faculties { get; set; }
		public virtual DbSet<People> People { get; set; }
		public virtual DbSet<Roles> Roles { get; set; }
		public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

			modelBuilder.Entity<People>()
				.Property(e => e.Scholarship)
				.HasPrecision(19, 4);

			modelBuilder.Entity<People>()
				.Property(e => e.Salary)
				.HasPrecision(19, 4);

			modelBuilder.Entity<People>()
				.HasMany(e => e.Dessertations)
				.WithOptional(e => e.People)
				.HasForeignKey(e => e.TeacherID);

			modelBuilder.Entity<People>()
				.HasMany(e => e.Faculties)
				.WithOptional(e => e.People)
				.HasForeignKey(e => e.DeanID);
		}
	}
}
