using Microsoft.EntityFrameworkCore;
using Real.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Data;

public class RealDbContext : DbContext
{
    public RealDbContext()
    {

    }

    public RealDbContext(DbContextOptions<RealDbContext> options)
        : base(options)
    {

    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (optionsBuilder.IsConfigured == false)
    //    {
    //        var dataSource = @"C:\temp\Real.db";

    //        optionsBuilder.UseSqlite($"Data Source={dataSource}");
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lancamento>()
            .HasDiscriminator(x => x.TipoLancamentoId)
            .HasValue<Movimento>(TipoLancamentoEnum.Movimento)
            .HasValue<FinancaAVista>(TipoLancamentoEnum.PagamentoAVista)
            .HasValue<Parcelamento>(TipoLancamentoEnum.Parcelamento)
            .HasValue<Parcela>(TipoLancamentoEnum.Parcela);

        modelBuilder.Entity<Apuracao>().HasKey(s => new { s.Competencia });
        modelBuilder.Entity<Apuracao>().Property(x => x.ValorPorCompetencia).HasPrecision(18, 2);
        modelBuilder.Entity<Apuracao>().Property(x => x.ValorPorData).HasPrecision(18, 2);

        modelBuilder.Entity<Lancamento>().Property(x => x.Valor).HasPrecision(18, 2);

        modelBuilder.Entity<Recorrencia>().Property(x => x.Valor).HasPrecision(18, 2);

        //modelBuilder.Entity<Financa>().Property(x => x.ValorPrevisto).HasPrecision(18, 2);

        modelBuilder.Entity<Categoria>().HasOne(x => x.Icon).WithMany().HasForeignKey(x => x.IconId).OnDelete(DeleteBehavior.Restrict);

        //

        SeedData.Execute(modelBuilder);

        //

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Lancamento> Lancamentos { get; set; }
    public DbSet<Recorrencia> Recorrencias { get; set; }
    public DbSet<Financa> Financas { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Apuracao> Apuracoes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Icon> Icons { get; set; }
}
