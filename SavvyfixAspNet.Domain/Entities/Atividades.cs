using System.ComponentModel.DataAnnotations;

namespace SavvyfixAspNet.Domain.Entities;

public class Atividades
{

    [Key]
    public long IdAtividades { get; set; }

    public string? ClimaAtual { get; set; }

    public string DemandaProduto { get; set; }

    public DateTime? HorarioAtual { get; set; }

    public string? LocalizacaoAtual { get; set; }

    public decimal PrecoVariado { get; set; }

    public int QntdProcura { get; set; }
    
    [Required(ErrorMessage = "O cliente é obrigatório.")]
    public long? IdCliente { get; set; }
    
    public Cliente Cliente { get; set; }
    
    public ICollection<Compra> Compras { get; set; }

    

}