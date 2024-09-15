using System.ComponentModel.DataAnnotations;

namespace SavvyfixAspNet.Domain.Entities;

public class Compra
{
    [Key]
    public long IdCompra { get; set; }
    
    [Required(ErrorMessage = "A quantidade de procura é obrigatória.")]
    public int QntdProd { get; set; }
    
    [Required(ErrorMessage = "O valor da compra é obrigatória.")]
    public decimal ValorCompra { get; set; }
    
    [Required(ErrorMessage = "O produto é obrigatório.")]
    public long? IdProd { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    public string NmProd { get; set; }
    
    [Required(ErrorMessage = "As especificações do produto são é obrigatórias.")]
    public string EspcificacoesProd { get; set; }
}