using System;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	public class PersonsDbContext : DbContext
	{
		public DbSet<Person> Persons { get; set; }
		public DbSet<Country> Countries { get; set; }

        public PersonsDbContext(DbContextOptions options) : base(options)
        {

        }

        // Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure dtabase table name
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Country>().ToTable("Countries");

            // Seed data
            string CountriesJson = System.IO.File.ReadAllText("countries.json");
            List<Country>? countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(CountriesJson);
            foreach (Country country in countries)
                modelBuilder.Entity<Country>().HasData(country);

            string PersonsJson = System.IO.File.ReadAllText("persons.json");
            List<Person>? persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(PersonsJson);
            foreach (Person person in persons)
                modelBuilder.Entity<Person>().HasData(person);
        }
    }
}

