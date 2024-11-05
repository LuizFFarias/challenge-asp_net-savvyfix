using Microsoft.ML;
using SavvyfixAspNet.ML;

public class PrevisaoService
{
   public static float AdicionarPorcentagens(AtividadeInputModel atividadeInput, PorcentagemCriterios criterios)
{
    // Verifique se a chave Localizacao existe no dicionário PorcentagemLocalizacao
    if (!criterios.PorcentagemLocalizacao.TryGetValue(atividadeInput.Localizacao, out double porcentagemLocalizacao))
    {
        porcentagemLocalizacao = 0; // ou trate como desejar se a chave não existir
    }

    // Verifique se a chave Horario existe no dicionário PorcentagemHorario
    if (!criterios.PorcentagemHorario.TryGetValue(atividadeInput.Horario, out double porcentagemHorario))
    {
        porcentagemHorario = 0; // ou trate como desejar
    }

    // Faça o mesmo para Clima, Procura e Demanda
    if (!criterios.PorcentagemClima.TryGetValue(atividadeInput.Clima.ToString(), out double porcentagemClima))
    {
        porcentagemClima = 0; // ou trate como desejar
    }

    if (!criterios.PorcentagemProcura.TryGetValue(atividadeInput.Procura.ToString(), out double porcentagemProcura))
    {
        porcentagemProcura = 0; // ou trate como desejar
    }

    if (!criterios.PorcentagemDemanda.TryGetValue(atividadeInput.Demanda, out double porcentagemDemanda))
    {
        porcentagemDemanda = 0; // ou trate como desejar
    }

    // Criar um novo objeto com as porcentagens preenchidas
    var atividadeData = new AtividadesDataComPorcentagem
    {
        Nome = atividadeInput.Nome,
        Produto = atividadeInput.Produto,
        Localizacao = atividadeInput.Localizacao,
        Horario = atividadeInput.Horario,
        Clima = atividadeInput.Clima,
        Procura = atividadeInput.Procura,
        Demanda = atividadeInput.Demanda,
        PorcentagemLocalizacao = (float)porcentagemLocalizacao,
        PorcentagemHorario = (float)porcentagemHorario,
        PorcentagemClima = (float)porcentagemClima,
        PorcentagemProcura = (float)porcentagemProcura,
        PorcentagemDemanda = (float)porcentagemDemanda,
        PrecoFinal = atividadeInput.PrecoBase // Use PrecoFinal ou ajuste conforme necessário
    };

    // Calcular a soma das porcentagens
    float somaPorcentagens = atividadeData.PorcentagemLocalizacao +
                             atividadeData.PorcentagemHorario +
                             atividadeData.PorcentagemClima +
                             atividadeData.PorcentagemProcura +
                             atividadeData.PorcentagemDemanda;

    // Calcular o preço final com base na soma das porcentagens e no preço base
    float precoFinal = atividadeData.PrecoFinal * (1 + somaPorcentagens / 100);
    
    return (float)Math.Round(precoFinal, 2);
}


}