using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SavvyfixAspNet.Api.Models;

[Serializable]
public record ProdutoAddOrUpdateModel
{
    public decimal PrecoFixo { get; set; } 

    public string MarcaProd { get; set; } = null!;

    public string DescProd { get; set; } = null!;

    public string NmProd { get; set; } = null!;

    public string Img { get; set; } = null!;

}
    