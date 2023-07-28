using Microsoft.EntityFrameworkCore;
using Npgsql;

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
            modelBuilder.Entity<Person>().ToTable("persons");
            modelBuilder.Entity<Country>().ToTable("countries");

            // Seed data
            string CountriesJson = System.IO.File.ReadAllText("countries.json");
            List<Country>? countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(CountriesJson);
            foreach (Country country in countries)
                modelBuilder.Entity<Country>().HasData(country);

            string PersonsJson = System.IO.File.ReadAllText("persons.json");
            List<Person>? persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(PersonsJson);
            foreach (Person person in persons)
                modelBuilder.Entity<Person>().HasData(person);

            // Fluent API
            // Edit column_name and column_type
            modelBuilder.Entity<Person>().Property(t => t.TIN)
                .HasColumnName("tax_identification_number")
                .HasColumnType("varchar(8)");
            // Add Column constraint
            modelBuilder.Entity<Person>()
                .HasCheckConstraint("CHK_TIN", "length(tax_identification_number) = 8");

            // Table relations
            //modelBuilder.Entity<Person>(entity =>
            //{
            //    entity.HasOne<Country>(c => c.country)
            //    .WithMany(p => p.persons)
            //    .HasForeignKey(p => p.country_id);
            //});
            // or just add 
        }

        public List<Person> sp_GetAllPersons()
        {
            // var persons = Persons.FromSqlRaw("Execute storedProcedureName").ToList(); // if using SQLServer
            var persons = Persons.FromSqlRaw("select * from sp_get_all_persons_func();").ToList();
            return persons;
        }

        public void sp_InsertPerson(Person person)
        {
            Database.ExecuteSqlRaw("Call sp_insert_person_proc(@person_id, @person_name, @email, @date_of_birth, @gender, @country_id, @address, @receive_newsletters)",
                new NpgsqlParameter("@person_id", person.person_id),
                new NpgsqlParameter("@person_name", person.person_name),
                new NpgsqlParameter("@email", person.email),
                new NpgsqlParameter("@date_of_birth", person.date_of_birth),
                new NpgsqlParameter("@gender", person.gender),
                new NpgsqlParameter("@country_id", person.country_id),
                new NpgsqlParameter("@address", person.address ?? String.Empty),
                new NpgsqlParameter("@receive_newsletters", person.receive_newsletters)
            );
        }

        public void sp_UpdatePerson(Person person)
        {
            Database.ExecuteSqlRaw("Call sp_update_person_proc(@person_id, @person_name, @email, @date_of_birth, @gender, @country_id, @address, @receive_newsletters)",
                new NpgsqlParameter("@person_id", person.person_id),
                new NpgsqlParameter("@person_name", person.person_name),
                new NpgsqlParameter("@email", person.email),
                new NpgsqlParameter("@date_of_birth", person.date_of_birth),
                new NpgsqlParameter("@gender", person.gender),
                new NpgsqlParameter("@country_id", person.country_id),
                new NpgsqlParameter("@address", person.address ?? String.Empty),
                new NpgsqlParameter("@receive_newsletters", person.receive_newsletters)
            );
        }

        public void sp_DeletePerson(Guid guid)
        {
            Database.ExecuteSqlRaw("Call sp_delete_person_proc(@person_id)", new NpgsqlParameter("@person_id", guid));
        }
    }
}

