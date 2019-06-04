using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiContribuinte.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cnae",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    codigo = table.Column<string>(nullable: true),
                    descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnae", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "natureza_juridica",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    codigo = table.Column<string>(nullable: true),
                    descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_natureza_juridica", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cnpj",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    data_abertura = table.Column<DateTime>(nullable: false),
                    natureza_juridica = table.Column<int>(nullable: false),
                    porte = table.Column<string>(nullable: true),
                    tipo_empresa = table.Column<string>(nullable: true),
                    razao_social = table.Column<string>(nullable: true),
                    fantasia = table.Column<string>(nullable: true),
                    cnpj = table.Column<string>(maxLength: 14, nullable: false),
                    email = table.Column<string>(nullable: true),
                    logradouro = table.Column<string>(nullable: true),
                    numero = table.Column<string>(nullable: true),
                    complemento = table.Column<string>(nullable: true),
                    cep = table.Column<string>(nullable: true),
                    bairro = table.Column<string>(nullable: true),
                    municipio = table.Column<string>(nullable: true),
                    uf = table.Column<string>(nullable: true),
                    situacao_rfb = table.Column<string>(nullable: true),
                    motivo_situacao_rfb = table.Column<string>(nullable: true),
                    data_situacao_rfb = table.Column<DateTime>(nullable: false),
                    validade_consulta = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_natureza_juridica_natureza_juridica",
                        column: x => x.natureza_juridica,
                        principalTable: "natureza_juridica",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cnpj_cnae",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    cnpj = table.Column<int>(nullable: false),
                    cnae = table.Column<int>(nullable: false),
                    principal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cnpj_cnae", x => x.id);
                    table.ForeignKey(
                        name: "FK_cnpj_cnae_cnae_cnae",
                        column: x => x.cnae,
                        principalTable: "cnae",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cnpj_cnae_cnpj_cnpj",
                        column: x => x.cnpj,
                        principalTable: "cnpj",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "telefone",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    cnpj = table.Column<int>(nullable: false),
                    numero = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telefone", x => x.id);
                    table.ForeignKey(
                        name: "FK_telefone_cnpj_cnpj",
                        column: x => x.cnpj,
                        principalTable: "cnpj",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_natureza_juridica",
                table: "cnpj",
                column: "natureza_juridica");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_cnae_cnae",
                table: "cnpj_cnae",
                column: "cnae");

            migrationBuilder.CreateIndex(
                name: "IX_cnpj_cnae_cnpj",
                table: "cnpj_cnae",
                column: "cnpj");

            migrationBuilder.CreateIndex(
                name: "IX_telefone_cnpj",
                table: "telefone",
                column: "cnpj");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cnpj_cnae");

            migrationBuilder.DropTable(
                name: "telefone");

            migrationBuilder.DropTable(
                name: "cnae");

            migrationBuilder.DropTable(
                name: "cnpj");

            migrationBuilder.DropTable(
                name: "natureza_juridica");
        }
    }
}
