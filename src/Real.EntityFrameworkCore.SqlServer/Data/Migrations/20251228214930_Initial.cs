using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Real.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Apuracoes",
                schema: "dbo",
                columns: table => new
                {
                    Competencia = table.Column<DateOnly>(type: "date", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    ValorPorCompetencia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ValorPorData = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apuracoes", x => x.Competencia);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoContaId = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: true),
                    Ativa = table.Column<bool>(type: "bit", nullable: false),
                    Pessoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaUnicode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BiClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BiUnicode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AplicaReceita = table.Column<bool>(type: "bit", nullable: false),
                    AplicaDespesa = table.Column<bool>(type: "bit", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: true),
                    Ativa = table.Column<bool>(type: "bit", nullable: true),
                    IconId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoriaPaiId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_CategoriaPaiId",
                        column: x => x.CategoriaPaiId,
                        principalSchema: "dbo",
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categorias_Icons_IconId",
                        column: x => x.IconId,
                        principalSchema: "dbo",
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recorrencia",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoLancamentoId = table.Column<int>(type: "int", nullable: false),
                    ContaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Competencia = table.Column<DateOnly>(type: "date", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: true),
                    TipoFinancaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TipoRecorrenciaId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recorrencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recorrencia_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalSchema: "dbo",
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recorrencia_Contas_ContaId",
                        column: x => x.ContaId,
                        principalSchema: "dbo",
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lancamentos",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoLancamentoId = table.Column<int>(type: "int", nullable: false),
                    TipoRegistroId = table.Column<int>(type: "int", nullable: false),
                    ContaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Competencia = table.Column<DateOnly>(type: "date", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: true),
                    EhGrupo = table.Column<bool>(type: "bit", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EhParcelamento = table.Column<bool>(type: "bit", nullable: false),
                    NumeroParcelas = table.Column<int>(type: "int", nullable: false),
                    EhParcela = table.Column<bool>(type: "bit", nullable: false),
                    ParcelamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    EhRecorrente = table.Column<bool>(type: "bit", nullable: false),
                    RecorrenciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    TipoFinancaId = table.Column<int>(type: "int", nullable: true),
                    CategoriaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EhPrevisao = table.Column<bool>(type: "bit", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lancamentos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalSchema: "dbo",
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lancamentos_Contas_ContaId",
                        column: x => x.ContaId,
                        principalSchema: "dbo",
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lancamentos_Lancamentos_GrupoId",
                        column: x => x.GrupoId,
                        principalSchema: "dbo",
                        principalTable: "Lancamentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lancamentos_Lancamentos_ParcelamentoId",
                        column: x => x.ParcelamentoId,
                        principalSchema: "dbo",
                        principalTable: "Lancamentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lancamentos_Recorrencia_RecorrenciaId",
                        column: x => x.RecorrenciaId,
                        principalSchema: "dbo",
                        principalTable: "Recorrencia",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Icons",
                columns: new[] { "Id", "BiClass", "BiUnicode", "FaClass", "FaUnicode", "Name" },
                values: new object[,]
                {
                    { "backpack", "backpack", "", "graduation-cap", "", "Backpack" },
                    { "bag", "bag", "", "bag-shopping", "", "Bag" },
                    { "book", "book", "", "book", "", "Book" },
                    { "bookmark", "bookmark", "", "bookmark", "", "Bookmark" },
                    { "bookmarks", "bookmarks", "", "book-bookmark", "", "Bookmarks" },
                    { "briefcase", "briefcase", "", "briefcase", "", "Briefcase" },
                    { "building", "building", "", "building", "", "Building" },
                    { "bus-front", "bus-front", "", "bus", "", "Bus Front" },
                    { "capsule", "capsule", "", "capsules", "", "Capsule" },
                    { "car-front", "car-front", "", "car", "", "Car Front" },
                    { "cart4", "cart4", "", "cart-shopping", "", "Cart Shopping" },
                    { "cash", "cash", "", "money-bill", "", "Cash" },
                    { "cash-coin", "cash-coin", "", "money-bills", "", "Cash Coin" },
                    { "cloud", "cloud", "", "cloud", "", "Cloud" },
                    { "cloud-arrow-up", "cloud-arrow-up", "", "cloud-arrow-up", "", "Cloud Arrow Up" },
                    { "coin", "coin", "", "coins", "", "Coin" },
                    { "currency-dollar", "currency-dollar", "", "dollar-sign", "$", "Currency Dollar" },
                    { "droplet", "droplet", "", "droplet", "", "Droplet" },
                    { "egg-fried", "egg-fried", "", "utensils", "", "Egg Fied" },
                    { "emoji-sunglasses", "emoji-sunglasses", "", "champagne-glasses", "", "Emoji Sunglasses" },
                    { "film", "film", "", "film", "", "Film" },
                    { "fire", "fire", "", "fire", "", "Fire" },
                    { "flower1", "flower1", "", "spray-can-sparkles", "", "Flower1" },
                    { "fuel-pump", "fuel-pump", "", "gas-pump", "", "Fuel Pump" },
                    { "globe-americas", "globe-americas", "", "earth-americas", "", "Globe Americas" },
                    { "heart-pulse", "heart-pulse", "", "heart-pulse", "", "Heart Pulse" },
                    { "house-heart", "house-heart", "", "house-circle-check", "", "House Heart" },
                    { "house-lock", "house-lock", "", "house-lock", "", "House Lock" },
                    { "lightbulb", "lightbulb", "", "lightbulb", "", "Lightbulb" },
                    { "music-player", "music-player", "", "music", "", "Music Player" },
                    { "p-circle", "p-circle", "", "square-parking", "", "P Circle" },
                    { "phone", "phone", "", "mobile-screen", "", "Phone" },
                    { "piggy-bank", "piggy-bank", "", "piggy-bank", "", "Piggy Bank" },
                    { "router", "router", "", "wifi", "", "Router" },
                    { "shop", "shop", "", "shop", "", "Shop" },
                    { "taxi-front", "taxi-front", "", "taxi", "", "Taxi Front" },
                    { "telephone", "telephone", "", "phone", "", "Telephone" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_CategoriaPaiId",
                schema: "dbo",
                table: "Categorias",
                column: "CategoriaPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_IconId",
                schema: "dbo",
                table: "Categorias",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamentos_CategoriaId",
                schema: "dbo",
                table: "Lancamentos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamentos_ContaId",
                schema: "dbo",
                table: "Lancamentos",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamentos_GrupoId",
                schema: "dbo",
                table: "Lancamentos",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamentos_ParcelamentoId",
                schema: "dbo",
                table: "Lancamentos",
                column: "ParcelamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamentos_RecorrenciaId",
                schema: "dbo",
                table: "Lancamentos",
                column: "RecorrenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recorrencia_CategoriaId",
                schema: "dbo",
                table: "Recorrencia",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recorrencia_ContaId",
                schema: "dbo",
                table: "Recorrencia",
                column: "ContaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apuracoes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Lancamentos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Recorrencia",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Categorias",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Contas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Icons",
                schema: "dbo");
        }
    }
}
