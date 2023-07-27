using Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class GetPersons_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Witre Stored Procedure
            string sp_GetAppPersons = @"
                create procedure get_all_persons()
                language sql
                as $$
                    select * from persons;
                $$;
            ";
            migrationBuilder.Sql(sp_GetAppPersons);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete Stored Procedure
            string sp_GetAppPersons = @"drop procedure get_all_persons();";
            migrationBuilder.Sql(sp_GetAppPersons);
        }
    }
}
