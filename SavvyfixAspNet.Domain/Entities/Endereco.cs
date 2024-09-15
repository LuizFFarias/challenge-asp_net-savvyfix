using System.ComponentModel.DataAnnotations;

namespace SavvyfixAspNet.Domain.Entities;

public class Endereco
{
    [Key]
    public long IdEndereco { get; set; }
    
    [Required(ErrorMessage = "O cep é obrigatório.")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter exatamente 8 números.")]
    public string CepEndereco { get; set; }

    [Required(ErrorMessage = "A rua é obrigatória.")]
    public string RuaEndereco { get; set; }
        
    [Required(ErrorMessage = "O número é obrigatório.")]
    public string NumEndereco { get; set; }
    
    [Required(ErrorMessage = "O bairro é obrigatório.")]
    public string BairroEndeereco { get; set; }
    
    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public string CidadeEndereco { get; set; }
    
    [Required(ErrorMessage = "O endereco é obrigatório.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Apenas as siglas do estado.")]
    public string EstadoEndereco { get; set; }
    
    [Required(ErrorMessage = "O pais é obrigatório.")]
    public string PaisEndereco { get; set; }
    
}