using Microsoft.ML.Data;

public class PrecificacaoData
{
    [LoadColumn(0)]
    public float Localizacao { get; set; }

    [LoadColumn(1)]
    public float Horario { get; set; }

    [LoadColumn(2)]
    public float Clima { get; set; }

    [LoadColumn(3)]
    public float Procura { get; set; }

    [LoadColumn(4)]
    public float Demanda { get; set; }

    [LoadColumn(5)]
    public float PrecoFinal { get; set; }  // Target (variável de saída)
}