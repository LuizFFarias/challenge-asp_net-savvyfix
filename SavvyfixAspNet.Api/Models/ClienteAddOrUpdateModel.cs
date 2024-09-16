namespace SavvyfixAspNet.Api.Models;

[Serializable]
public record ClienteAddOrUpdateModel
{
    public string CpfClie { get; set; } = null!;
    
    public string NmClie { get; set; } = null!;

    public string SenhaClie { get; set; } = null!;
    
    public long? IdEndereco { get; set; }
    
}