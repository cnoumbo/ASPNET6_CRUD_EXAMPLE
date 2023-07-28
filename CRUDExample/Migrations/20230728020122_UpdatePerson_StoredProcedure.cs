using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePerson_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_query = @"
                create or replace procedure sp_update_person_proc(c_person_id uuid, c_person_name varchar(40), c_email varchar(50), c_date_of_birth date, c_gender varchar(10), c_country_id uuid, c_address varchar(200), c_receive_newsletters boolean)
                language plpgsql
                as $$
                begin
                    update persons
                    set person_name = c_person_name,
                    email = c_email,
                    date_of_birth = c_date_of_birth,
                    gender = c_gender,
                    country_id = c_country_id,
                    address = c_address,
                    receive_newsletters = c_receive_newsletters
                    where person_id = c_person_id;

                    commit;
                end
                $$;
            ";
            migrationBuilder.Sql(sp_query);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete Stored Procedure
            string sp_query = @"drop procedure sp_update_person_proc(uuid, varchar, varchar, date, varchar, uuid, varchar, boolean);";
            migrationBuilder.Sql(sp_query);
        }
    }
}
