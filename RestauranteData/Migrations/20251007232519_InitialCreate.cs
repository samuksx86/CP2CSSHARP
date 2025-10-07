using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ENDERECO = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    COMPLEMENTO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    DATACADASTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTOS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    PRECO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CATEGORIA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DISPONIVEL = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DATACADASTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    DATACADASTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDOS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CLIENTEID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DATAPEDIDO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STATUS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    VALORTOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OBSERVACOES = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDOS_CLIENTES_CLIENTEID",
                        column: x => x.CLIENTEID,
                        principalTable: "CLIENTES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ENTREGAS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PEDIDOID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    NOMEMOTOBOY = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    TELEFONEMOTOBOY = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DATASAIDA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    DATAENTREGA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    OBSERVACOESENTREGA = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTREGAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ENTREGAS_PEDIDOS_PEDIDOID",
                        column: x => x.PEDIDOID,
                        principalTable: "PEDIDOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO_ITENS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PEDIDOID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PRODUTOID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    QUANTIDADE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PRECOUNITARIO = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_ITENS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDO_ITENS_PEDIDOS_PEDIDOID",
                        column: x => x.PEDIDOID,
                        principalTable: "PEDIDOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PEDIDO_ITENS_PRODUTOS_PRODUTOID",
                        column: x => x.PRODUTOID,
                        principalTable: "PRODUTOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ENTREGAS_PEDIDOID",
                table: "ENTREGAS",
                column: "PEDIDOID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_ITENS_PEDIDOID",
                table: "PEDIDO_ITENS",
                column: "PEDIDOID");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_ITENS_PRODUTOID",
                table: "PEDIDO_ITENS",
                column: "PRODUTOID");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDOS_CLIENTEID",
                table: "PEDIDOS",
                column: "CLIENTEID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENTREGAS");

            migrationBuilder.DropTable(
                name: "PEDIDO_ITENS");

            migrationBuilder.DropTable(
                name: "USUARIOS");

            migrationBuilder.DropTable(
                name: "PEDIDOS");

            migrationBuilder.DropTable(
                name: "PRODUTOS");

            migrationBuilder.DropTable(
                name: "CLIENTES");
        }
    }
}
