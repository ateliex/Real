using Microsoft.EntityFrameworkCore;
using Real.Models;

namespace Real.Data;

public static class SeedData
{
    public static void Execute(ModelBuilder modelBuilder)
    {
        SeedIcons(modelBuilder);
        SeedCategorias(modelBuilder);
        SeedContas(modelBuilder);
    }

    public static void SeedIcons(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "shop", Name = "Shop", FaClass = "shop", BiClass = "shop", FaUnicode = "\uf54f", BiUnicode = "\uf543" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "cart4", Name = "Cart Shopping", FaClass = "cart-shopping", BiClass = "cart4", FaUnicode = "\uf07a", BiUnicode = "\uf245" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "cash-coin", Name = "Cash Coin", FaClass = "money-bills", BiClass = "cash-coin", FaUnicode = "\ue1f3", BiUnicode = "\uf246" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "currency-dollar", Name = "Currency Dollar", FaClass = "dollar-sign", BiClass = "currency-dollar", FaUnicode = "\u0024", BiUnicode = "\uf636" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "coin", Name = "Coin", FaClass = "coins", BiClass = "coin", FaUnicode = "\uf51e", BiUnicode = "\uf634" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "cash", Name = "Cash", FaClass = "money-bill", BiClass = "cash", FaUnicode = "\uf0d6", BiUnicode = "\uf247" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "house-lock", Name = "House Lock", FaClass = "house-lock", BiClass = "house-lock", FaUnicode = "\ue510", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "piggy-bank", Name = "Piggy Bank", FaClass = "piggy-bank", BiClass = "piggy-bank", FaUnicode = "\uf4d3", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "house-heart", Name = "House Heart", FaClass = "house-circle-check", BiClass = "house-heart", FaUnicode = "\ue509", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "building", Name = "Building", FaClass = "building", BiClass = "building", FaUnicode = "\uf1ad", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "droplet", Name = "Droplet", FaClass = "droplet", BiClass = "droplet", FaUnicode = "\uf043", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "lightbulb", Name = "Lightbulb", FaClass = "lightbulb", BiClass = "lightbulb", FaUnicode = "\uf0eb", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "fire", Name = "Fire", FaClass = "fire", BiClass = "fire", FaUnicode = "\uf06d", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "telephone", Name = "Telephone", FaClass = "phone", BiClass = "telephone", FaUnicode = "\uf095", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "router", Name = "Router", FaClass = "wifi", BiClass = "router", FaUnicode = "\uf1eb", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "capsule", Name = "Capsule", FaClass = "capsules", BiClass = "capsule", FaUnicode = "\uf46b", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "taxi-front", Name = "Taxi Front", FaClass = "taxi", BiClass = "taxi-front", FaUnicode = "\uf1ba", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "film", Name = "Film", FaClass = "film", BiClass = "film", FaUnicode = "\uf008", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "phone", Name = "Phone", FaClass = "mobile-screen", BiClass = "phone", FaUnicode = "\uf3cf", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "music-player", Name = "Music Player", FaClass = "music", BiClass = "music-player", FaUnicode = "\uf001", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "cloud-arrow-up", Name = "Cloud Arrow Up", FaClass = "cloud-arrow-up", BiClass = "cloud-arrow-up", FaUnicode = "\uf0ee", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "cloud", Name = "Cloud", FaClass = "cloud", BiClass = "cloud", FaUnicode = "\uf0c2", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "briefcase", Name = "Briefcase", FaClass = "briefcase", BiClass = "briefcase", FaUnicode = "\uf0b1", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "globe-americas", Name = "Globe Americas", FaClass = "earth-americas", BiClass = "globe-americas", FaUnicode = "\uf57d", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "backpack", Name = "Backpack", FaClass = "graduation-cap", BiClass = "backpack", FaUnicode = "\uf19d", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "book", Name = "Book", FaClass = "book", BiClass = "book", FaUnicode = "\uf02d", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "heart-pulse", Name = "Heart Pulse", FaClass = "heart-pulse", BiClass = "heart-pulse", FaUnicode = "\uf21e", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "bag", Name = "Bag", FaClass = "bag-shopping", BiClass = "bag", FaUnicode = "\uf290", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "egg-fried", Name = "Egg Fied", FaClass = "utensils", BiClass = "egg-fried", FaUnicode = "\uf2e7", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "car-front", Name = "Car Front", FaClass = "car", BiClass = "car-front", FaUnicode = "\uf1b9", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "fuel-pump", Name = "Fuel Pump", FaClass = "gas-pump", BiClass = "fuel-pump", FaUnicode = "\uf52f", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "p-circle", Name = "P Circle", FaClass = "square-parking", BiClass = "p-circle", FaUnicode = "\uf540", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "bus-front", Name = "Bus Front", FaClass = "bus", BiClass = "bus-front", FaUnicode = "\uf207", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "emoji-sunglasses", Name = "Emoji Sunglasses", FaClass = "champagne-glasses", BiClass = "emoji-sunglasses", FaUnicode = "\uf79f", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "flower1", Name = "Flower1", FaClass = "spray-can-sparkles", BiClass = "flower1", FaUnicode = "\uf5d0", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "bookmarks", Name = "Bookmarks", FaClass = "book-bookmark", BiClass = "bookmarks", FaUnicode = "\ue0bb", BiUnicode = "" });
        modelBuilder.Entity<Icon>().HasData(new Icon { Id = "bookmark", Name = "Bookmark", FaClass = "bookmark", BiClass = "bookmark", FaUnicode = "\uf02e", BiUnicode = "" });
    }

    public static void SeedCategorias(ModelBuilder modelBuilder)
    {
        var ordem = 0;

        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = true, AplicaDespesa = false, Ordem = ordem++, Id = "salario               ", Nome = "Salário", IconId = "cash-coin" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = true, AplicaDespesa = false, Ordem = ordem++, Id = "balanco               ", Nome = "Balanço", IconId = "currency-dollar" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "imposto               ", Nome = "Imposto", IconId = "coin" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "multa                 ", Nome = "Multa", IconId = "currency-dollar" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "juros                 ", Nome = "Juros", IconId = "currency-dollar" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "taxa                  ", Nome = "Taxa", IconId = "currency-dollar" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = true, AplicaDespesa = true, Ordem = ordem++, Id = "emprestimo            ", Nome = "Empréstimo", IconId = "cash" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "seguro                ", Nome = "Seguro", IconId = "house-lock" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "reserva               ", Nome = "Reserva", IconId = "piggy-bank" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "casa                  ", Nome = "Casa", IconId = "house-heart" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "moradia               ", Nome = "Moradia", CategoriaPaiId = "casa                  ", IconId = "house-heart" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "aluguel               ", Nome = "Aluguel", CategoriaPaiId = "moradia               ", IconId = "house-heart" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "condominio            ", Nome = "Condomínio", CategoriaPaiId = "moradia               ", IconId = "building" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "agua                  ", Nome = "Água", CategoriaPaiId = "casa                  ", IconId = "droplet" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "luz                   ", Nome = "Luz", CategoriaPaiId = "casa                  ", IconId = "lightbulb" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "gas                   ", Nome = "Gás", CategoriaPaiId = "casa                  ", IconId = "fire" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "telefone              ", Nome = "Telefone", CategoriaPaiId = "casa                  ", IconId = "telephone" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "internet              ", Nome = "Internet", CategoriaPaiId = "casa                  ", IconId = "router" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "celular               ", Nome = "Celular", IconId = "phone" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "stream                ", Nome = "Stream", IconId = "music-player" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "storage               ", Nome = "Storage", IconId = "cloud-arrow-up" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "assinatura            ", Nome = "Assinatura", IconId = "cloud" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "profissional          ", Nome = "Profissional", IconId = "briefcase" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "dominio               ", Nome = "Domínio", IconId = "globe-americas" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "contabilidade         ", Nome = "Contabilidade", IconId = "briefcase" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "educacao              ", Nome = "Educação", IconId = "backpack" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "livro                 ", Nome = "Livro", IconId = "book" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "saude                 ", Nome = "Saúde", IconId = "heart-pulse" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "plano-de-saude        ", Nome = "Plano de Saúde", CategoriaPaiId = "saude                 ", IconId = "heart-pulse" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "plano-odontologico    ", Nome = "Plano Odontológico", CategoriaPaiId = "saude                 ", IconId = "heart-pulse" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "medico                ", Nome = "Médico", CategoriaPaiId = "saude                 ", IconId = "heart-pulse" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "dentista              ", Nome = "Dentista", CategoriaPaiId = "saude                 ", IconId = "heart-pulse" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "pscicologo            ", Nome = "Pscicólogo", CategoriaPaiId = "saude                 ", IconId = "heart-pulse" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "academia              ", Nome = "Academia", IconId = "heart-pulse" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "farmacia              ", Nome = "Farmácia", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "medicamento           ", Nome = "Medicamento", CategoriaPaiId = "saude                 ", IconId = "capsule" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "suplemento            ", Nome = "Suplemento", CategoriaPaiId = "saude                 ", IconId = "capsule" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "compra                ", Nome = "Compra", IconId = "bag" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "mercado               ", Nome = "Mercado", IconId = "cart4" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "leite-de-formula      ", Nome = "Leite de Fórmula", IconId = "cart4" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "refeicao              ", Nome = "Refeição", IconId = "egg-fried" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "restaurante           ", Nome = "Restaurante", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "padaria               ", Nome = "Padaria", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "lanche                ", Nome = "Lanche", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "hamburger             ", Nome = "Hamburger", CategoriaPaiId = "lanche                ", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "pizza                 ", Nome = "Pizza", CategoriaPaiId = "lanche                ", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "sorvete               ", Nome = "Sorvete", CategoriaPaiId = "lanche                ", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "pet                   ", Nome = "Pet", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "roupa                 ", Nome = "Roupa", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "presente              ", Nome = "Presente", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "brinquedo             ", Nome = "Brinquedo", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "beleza                ", Nome = "Beleza", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "salao-de-beleza       ", Nome = "Salão de Beleza", CategoriaPaiId = "beleza                ", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "barbeiro              ", Nome = "Barbeiro", CategoriaPaiId = "beleza                ", IconId = "shop" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "carro                 ", Nome = "Carro", IconId = "car-front" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "combustivel           ", Nome = "Combustível", IconId = "fuel-pump" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "estacionamento        ", Nome = "Estacionamento", IconId = "p-circle" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "pedagio               ", Nome = "Pedágio", IconId = "car-front" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "lavajato              ", Nome = "Lavajato", IconId = "car-front" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "transporte            ", Nome = "Transporte", IconId = "bus-front" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "passagem              ", Nome = "Passagem", CategoriaPaiId = "transporte            ", IconId = "bus-front" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "taxi                  ", Nome = "Taxi", CategoriaPaiId = "transporte            ", IconId = "taxi-front" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "lazer                 ", Nome = "Lazer", IconId = "emoji-sunglasses" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "cinema                ", Nome = "Cinema", CategoriaPaiId = "lazer                 ", IconId = "film" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "perfume               ", Nome = "Perfume", IconId = "flower1" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "diversos              ", Nome = "Diversos", IconId = "bookmarks" });
        modelBuilder.Entity<Categoria>().HasData(new Categoria { AplicaReceita = false, AplicaDespesa = true, Ordem = ordem++, Id = "outros                ", Nome = "Outros", IconId = "bookmark" });
    }

    public static void SeedContas(ModelBuilder modelBuilder)
    {
        var ordem = 0;

        int t = 0, r = 0;

        t = 1; r = 0;

        ordem = 0;

        var conta_Carteira = new Conta { Id = NewId(t, r++), Ordem = ordem++, Nome = "Carteira" };

        modelBuilder.Entity<Conta>().HasData(conta_Carteira);
    }

    private static Guid NewId(int t, int r)
    {
        return new Guid($"{t:00000000}-0000-4000-8000-{r:000000000000}");
    }
}
