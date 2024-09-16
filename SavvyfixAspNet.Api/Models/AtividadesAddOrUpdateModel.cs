namespace SavvyfixAspNet.Api.Models;

public class AtividadesAddOrUpdateModel
{
    public string? ClimaAtual { get; set; }
    
    public string DemandaProduto { get; set; }
    
    public DateTime? HorarioAtual { get; set; }
    
    public string? LocalizacaoAtual { get; set; }
    
    public decimal PrecoVariado { get; set; }
    
    public int QntdProcura { get; set; }
    
    public long? IdCliente { get; set; }

}