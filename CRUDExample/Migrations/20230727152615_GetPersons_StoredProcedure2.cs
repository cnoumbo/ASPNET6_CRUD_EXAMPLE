using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class GetPersons_StoredProcedure2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_query = @"
                create or replace function sp_get_all_persons_func()
                returns setof persons
                language sql
                as $$
                    select * from persons;
                $$;
            ";
            migrationBuilder.Sql(sp_query);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete Stored Procedure
            string sp_query = @"drop function sp_get_all_persons_func();";
            migrationBuilder.Sql(sp_query);
        }
    }
}
