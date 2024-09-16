namespace SavvyfixAspNet.Api.Models;

public record CompraAddOrUpdateModel
{
    public int QntdProd { get; set; }
    
    public long? IdProd { get; set; }

    public long? IdCliente { get; set; }
    
    public long? IdAtividades { get; set; }
    
    public string NmProd { get; set; }
    
    public string EspcificacoesProd { get; set; }
}