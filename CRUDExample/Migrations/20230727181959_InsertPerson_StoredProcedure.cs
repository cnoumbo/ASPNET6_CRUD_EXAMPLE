using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class InsertPerson_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_query = @"
                create or replace procedure sp_insert_person_proc(c_person_id uuid, c_person_name varchar(40), c_email varchar(50), c_date_of_birth date, c_gender varchar(10), c_country_id uuid, c_address varchar(200), c_receive_newsletters boolean)
                language plpgsql as
                $$
                begin
                    insert into persons(person_id, person_name, email, date_of_birth, gender, country_id, address, receive_newsletters) values (c_person_id, c_person_name, c_email, c_date_of_birth, c_gender, c_country_id, c_address, c_receive_newsletters);
                end
                $$;
            ";
            migrationBuilder.Sql(sp_query);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete Stored Procedure
            string sp_query = @"drop procedure sp_insert_person_proc(uuid, varchar, varchar, date, varchar, uuid, varchar, boolean);";
            migrationBuilder.Sql(sp_query);
        }
    }
}
