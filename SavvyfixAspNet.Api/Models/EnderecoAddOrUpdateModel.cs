namespace SavvyfixAspNet.Api.Models;

public record EnderecoAddOrUpdateModel
{
    public string CepEndereco { get; set; }
    
    public string RuaEndereco { get; set; }
    
    public string NumEndereco { get; set; }
    
    public string BairroEndeereco { get; set; }
    
    public string CidadeEndereco { get; set; }
    
    public string EstadoEndereco { get; set; }
    
    public string PaisEndereco { get; set; }
    
}