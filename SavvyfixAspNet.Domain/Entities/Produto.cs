using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SavvyfixAspNet.Domain.Entities;

public class Produto
{
    [Key]
    [Column("id_prod")]
    public long IdProd { get; set; }
    
    [Required]
    [Column("preco_fixo")]
    public decimal PrecoFixo { get; set; }
    
    [Required(ErrorMessage = "A marca é obrigatória.")]
    [Column("marca_prod")]
    public string MarcaProd { get; set; } = null!;

    [Required(ErrorMessage = "A descrição do produto é obrigatório.")]
    [Column("desc_proc")]
    public string DescProd { get; set; } = null!;

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [Column("nm_prod")]
    public string NmProd { get; set; } = null!;
    
    [Required(ErrorMessage = "A imagem do produto é obrigatória.")]
    [Column("img_prod")]
    public string Img { get; set; } 
    
    public ICollection<Compra> Compras { get; set; }
}