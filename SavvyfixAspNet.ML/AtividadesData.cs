using Microsoft.ML.Data;

public class AtividadesData
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
    
    public double PorcentagemLocalizacao { get; set; }
    public double PorcentagemHorario { get; set; }
    public double PorcentagemClima { get; set; }
    public double PorcentagemProcura { get; set; }
    public double PorcentagemDemanda { get; set; }
    
    public double Resultado { get; set; }
}