using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class DeletePerson_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_query = @"
                create or replace procedure sp_delete_person_proc(c_person_id uuid)
                language plpgsql as
                $$
                begin
                    delete from persons
                    where person_id = c_person_id;
                end
                $$;
            ";
            migrationBuilder.Sql(sp_query);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete Stored Procedure
            string sp_query = @"drop procedure sp_delete_person_proc(uuid);";
            migrationBuilder.Sql(sp_query);
        }
    }
}
