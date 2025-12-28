using Real.Models;

namespace Real.Models;

public class ApuracaoMensalInput
{
    public DateTime? Competencia { get; set; }

    public ModoVisualizacaoEnum? ModoVisualizacao { get; set; }

    public OrdemEnum? Ordem { get; set; }

    public RegimeApuracaoEnum? RegimeApuracao { get; set; }

    public bool? ExibirTodasCategorias { get; set; }
}

public class ApuracaoMensalOutput
{
    public decimal ValorReceitas { get; set; }

    public decimal ValorDespesas { get; set; }

    public decimal ValorSaldo { get; set; }

    public decimal ValorSaldoAcumulado { get; set; }

    public decimal ValorSaldoTotal { get; set; }

    public IEnumerable<ApuracaoCategoriaOutput> Receitas { get; set; } = new HashSet<ApuracaoCategoriaOutput>();

    public IEnumerable<ApuracaoCategoriaOutput> Despesas { get; set; } = new HashSet<ApuracaoCategoriaOutput>();
}

public class ApuracaoCategoriaOutput
{
    public required string Nome { get; set; }

    public decimal Total { get; set; }
}
