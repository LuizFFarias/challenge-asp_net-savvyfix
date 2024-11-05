using Microsoft.ML.Data;

public class AtividadesDataComPorcentagem
{
    [LoadColumn(0)]
    public string Nome { get; set; }
    
    [LoadColumn(1)]
    public string Produto { get; set; }
    
    [LoadColumn(2)]
    public string Localizacao { get; set; }
    
    [LoadColumn(3)]
    public string Horario { get; set; }
    
    [LoadColumn(4)]
    public float Clima { get; set; }
    
    [LoadColumn(5)]
    public float Procura { get; set; }
    
    [LoadColumn(6)]
    public string Demanda { get; set; }
    
    [LoadColumn(7)]
    public float PorcentagemLocalizacao { get; set; }
    
    [LoadColumn(8)]
    public float PorcentagemHorario { get; set; }
    
    [LoadColumn(9)]
    public float PorcentagemClima { get; set; }
    
    [LoadColumn(10)]
    public float PorcentagemProcura { get; set; }
    
    [LoadColumn(11)]
    public float PorcentagemDemanda { get; set; }

    [LoadColumn(12)]
    public float PrecoFinal { get; set; } 
}