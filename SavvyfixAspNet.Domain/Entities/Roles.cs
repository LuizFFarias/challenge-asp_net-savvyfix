using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SavvyfixAspNet.Domain.Entities;

public class Roles
{
    [Key]
    [Column("id_role")]
    public long IdRole { get; set; }
    
    [Required]
    public string NomeRole { get; set; }
    
    [JsonIgnore]
    public ICollection<ClienteRole> ClienteRoles { get; set; } = new List<ClienteRole>();
}