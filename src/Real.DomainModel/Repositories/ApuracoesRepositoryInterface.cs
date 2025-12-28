using Real.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real.Repositories;

public interface ApuracoesRepositoryInterface
{
    Task<Apuracao> ObtemApuracao(DateOnly competencia);

    Task<Apuracao?> ObtemApuracaoOrDefault(DateOnly competencia);

    Task Adiciona(Apuracao apuracao);

    Task Atualiza(Apuracao apuracao);

    Task Exclui(Apuracao apuracao);
}
