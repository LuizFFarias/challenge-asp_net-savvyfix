using System.ComponentModel.DataAnnotations;

namespace SavvyfixAspNet.Domain.Entities;

public class Produto
{
    [Key]
    public long IdProd { get; set; }
    
    [Required]
    public decimal PrecoFixo { get; set; }
    
    [Required(ErrorMessage = "A marca é obrigatória.")]
    public string MarcaProd { get; set; } = null!;

    [Required(ErrorMessage = "A descrição do produto é obrigatório.")]
    public string DescProd { get; set; } = null!;

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    public string NmProd { get; set; } = null!;
    
    [Required(ErrorMessage = "A imagem do produto é obrigatória.")]
    public string Img { get; set; } 
}