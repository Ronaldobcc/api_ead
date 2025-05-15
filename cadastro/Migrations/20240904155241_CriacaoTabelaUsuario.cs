using Microsoft.EntityFrameworkCore.Migrations;
using System;
#nullable disable

namespace cadastro.Migrations
{
    public partial class CriacaoTabelaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "varchar(20)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: true),
                    Login = table.Column<string>(type: "varchar(255)", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    Perfil = table.Column<int>(type: "varchar(255)", nullable: false),
                    Senha = table.Column<string>(type: "varchar(255)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "varchar(255)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
