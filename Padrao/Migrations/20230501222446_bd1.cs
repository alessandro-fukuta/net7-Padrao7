using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Padrao.Migrations
{
    /// <inheritdoc />
    public partial class bd1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NomeCompleto = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    NomeUsuario = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Celular = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Senha = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false),
                    Cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Administrador = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Chave = table.Column<string>(type: "longtext", nullable: true),
                    Autenticacao = table.Column<int>(type: "int", nullable: false),
                    EmailValidado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Endereco_Cep = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Rua = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Numero = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Bairro = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Cidade = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Estado = table.Column<string>(type: "longtext", nullable: true),
                    CPF = table.Column<string>(type: "longtext", nullable: true),
                    RG = table.Column<string>(type: "longtext", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario_Dados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Endereco_Cep = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Rua = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Numero = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Bairro = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Cidade = table.Column<string>(type: "longtext", nullable: true),
                    Endereco_Estado = table.Column<string>(type: "longtext", nullable: true),
                    CPF = table.Column<string>(type: "longtext", nullable: true),
                    RG = table.Column<string>(type: "longtext", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Dados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Dados_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Dados_UsuarioID",
                table: "Usuario_Dados",
                column: "UsuarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario_Dados");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
