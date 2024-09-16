using System.ComponentModel.DataAnnotations;

namespace SavvyfixAspNet.Domain.Entities;

public class Cliente
{
    [Key]
    public long IdCliente { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 números.")]
    public string CpfClie { get; set; } = null!;

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string NmClie { get; set; } = null!;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string SenhaClie { get; set; } = null!;
    
    [Required(ErrorMessage = "O endereco é obrigatório.")]
    public long? IdEndereco { get; set; }
    
    
    public Endereco Endereco { get; set; }
    
    public ICollection<Compra> Compras { get; set; }
    
    public ICollection<Atividades> Atividades { get; set; }
}