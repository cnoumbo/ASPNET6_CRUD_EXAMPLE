using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class GetPersons_StoredProcedureEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Witre Stored Procedure
            string sp_GetAppPersons = @"
                create or replace procedure get_all_persons()
                language sql
                as $$
                    select * from public.persons;
                $$;
            ";
            migrationBuilder.Sql(sp_GetAppPersons);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
