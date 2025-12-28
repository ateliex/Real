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
                table: "Contas",
                columns: new[] { "Id", "Ativa", "CreationDate", "Nome", "Ordem", "Pessoa", "TipoContaId" },
                values: new object[] { new Guid("00000001-0000-4000-8000-000000000000"), false, null, "Carteira", 0, null, 0 });

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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Categorias",
                columns: new[] { "Id", "AplicaDespesa", "AplicaReceita", "Ativa", "CategoriaPaiId", "CreationDate", "IconId", "Nome", "Ordem" },
                values: new object[,]
                {
                    { "academia              ", true, false, null, null, null, "heart-pulse", "Academia", 33 },
                    { "assinatura            ", true, false, null, null, null, "cloud", "Assinatura", 21 },
                    { "balanco               ", false, true, null, null, null, "currency-dollar", "Balanço", 1 },
                    { "beleza                ", true, false, null, null, null, "shop", "Beleza", 51 },
                    { "brinquedo             ", true, false, null, null, null, "shop", "Brinquedo", 50 },
                    { "carro                 ", true, false, null, null, null, "car-front", "Carro", 54 },
                    { "casa                  ", true, false, null, null, null, "house-heart", "Casa", 9 },
                    { "celular               ", true, false, null, null, null, "phone", "Celular", 18 },
                    { "combustivel           ", true, false, null, null, null, "fuel-pump", "Combustível", 55 },
                    { "compra                ", true, false, null, null, null, "bag", "Compra", 37 },
                    { "contabilidade         ", true, false, null, null, null, "briefcase", "Contabilidade", 24 },
                    { "diversos              ", true, false, null, null, null, "bookmarks", "Diversos", 65 },
                    { "dominio               ", true, false, null, null, null, "globe-americas", "Domínio", 23 },
                    { "educacao              ", true, false, null, null, null, "backpack", "Educação", 25 },
                    { "emprestimo            ", true, true, null, null, null, "cash", "Empréstimo", 6 },
                    { "estacionamento        ", true, false, null, null, null, "p-circle", "Estacionamento", 56 },
                    { "farmacia              ", true, false, null, null, null, "shop", "Farmácia", 34 },
                    { "imposto               ", true, false, null, null, null, "coin", "Imposto", 2 },
                    { "juros                 ", true, false, null, null, null, "currency-dollar", "Juros", 4 },
                    { "lanche                ", true, false, null, null, null, "shop", "Lanche", 43 },
                    { "lavajato              ", true, false, null, null, null, "car-front", "Lavajato", 58 },
                    { "lazer                 ", true, false, null, null, null, "emoji-sunglasses", "Lazer", 62 },
                    { "leite-de-formula      ", true, false, null, null, null, "cart4", "Leite de Fórmula", 39 },
                    { "livro                 ", true, false, null, null, null, "book", "Livro", 26 },
                    { "mercado               ", true, false, null, null, null, "cart4", "Mercado", 38 },
                    { "multa                 ", true, false, null, null, null, "currency-dollar", "Multa", 3 },
                    { "outros                ", true, false, null, null, null, "bookmark", "Outros", 66 },
                    { "padaria               ", true, false, null, null, null, "shop", "Padaria", 42 },
                    { "pedagio               ", true, false, null, null, null, "car-front", "Pedágio", 57 },
                    { "perfume               ", true, false, null, null, null, "flower1", "Perfume", 64 },
                    { "pet                   ", true, false, null, null, null, "shop", "Pet", 47 },
                    { "presente              ", true, false, null, null, null, "shop", "Presente", 49 },
                    { "profissional          ", true, false, null, null, null, "briefcase", "Profissional", 22 },
                    { "refeicao              ", true, false, null, null, null, "egg-fried", "Refeição", 40 },
                    { "reserva               ", true, false, null, null, null, "piggy-bank", "Reserva", 8 },
                    { "restaurante           ", true, false, null, null, null, "shop", "Restaurante", 41 },
                    { "roupa                 ", true, false, null, null, null, "shop", "Roupa", 48 },
                    { "salario               ", false, true, null, null, null, "cash-coin", "Salário", 0 },
                    { "saude                 ", true, false, null, null, null, "heart-pulse", "Saúde", 27 },
                    { "seguro                ", true, false, null, null, null, "house-lock", "Seguro", 7 },
                    { "storage               ", true, false, null, null, null, "cloud-arrow-up", "Storage", 20 },
                    { "stream                ", true, false, null, null, null, "music-player", "Stream", 19 },
                    { "taxa                  ", true, false, null, null, null, "currency-dollar", "Taxa", 5 },
                    { "transporte            ", true, false, null, null, null, "bus-front", "Transporte", 59 },
                    { "agua                  ", true, false, null, "casa                  ", null, "droplet", "Água", 13 },
                    { "barbeiro              ", true, false, null, "beleza                ", null, "shop", "Barbeiro", 53 },
                    { "cinema                ", true, false, null, "lazer                 ", null, "film", "Cinema", 63 },
                    { "dentista              ", true, false, null, "saude                 ", null, "heart-pulse", "Dentista", 31 },
                    { "gas                   ", true, false, null, "casa                  ", null, "fire", "Gás", 15 },
                    { "hamburger             ", true, false, null, "lanche                ", null, "shop", "Hamburger", 44 },
                    { "internet              ", true, false, null, "casa                  ", null, "router", "Internet", 17 },
                    { "luz                   ", true, false, null, "casa                  ", null, "lightbulb", "Luz", 14 },
                    { "medicamento           ", true, false, null, "saude                 ", null, "capsule", "Medicamento", 35 },
                    { "medico                ", true, false, null, "saude                 ", null, "heart-pulse", "Médico", 30 },
                    { "moradia               ", true, false, null, "casa                  ", null, "house-heart", "Moradia", 10 },
                    { "passagem              ", true, false, null, "transporte            ", null, "bus-front", "Passagem", 60 },
                    { "pizza                 ", true, false, null, "lanche                ", null, "shop", "Pizza", 45 },
                    { "plano-de-saude        ", true, false, null, "saude                 ", null, "heart-pulse", "Plano de Saúde", 28 },
                    { "plano-odontologico    ", true, false, null, "saude                 ", null, "heart-pulse", "Plano Odontológico", 29 },
                    { "pscicologo            ", true, false, null, "saude                 ", null, "heart-pulse", "Pscicólogo", 32 },
                    { "salao-de-beleza       ", true, false, null, "beleza                ", null, "shop", "Salão de Beleza", 52 },
                    { "sorvete               ", true, false, null, "lanche                ", null, "shop", "Sorvete", 46 },
                    { "suplemento            ", true, false, null, "saude                 ", null, "capsule", "Suplemento", 36 },
                    { "taxi                  ", true, false, null, "transporte            ", null, "taxi-front", "Taxi", 61 },
                    { "telefone              ", true, false, null, "casa                  ", null, "telephone", "Telefone", 16 },
                    { "aluguel               ", true, false, null, "moradia               ", null, "house-heart", "Aluguel", 11 },
                    { "condominio            ", true, false, null, "moradia               ", null, "building", "Condomínio", 12 }
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
