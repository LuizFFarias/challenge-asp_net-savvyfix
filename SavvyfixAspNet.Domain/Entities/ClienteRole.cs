namespace SavvyfixAspNet.Domain.Entities;

public class ClienteRole
{
    public long IdCliete { get; set; }
    public Cliente Cliente { get; set; }
    
    public long IdRole { get; set; }
    public Roles Roles { get; set; }
}