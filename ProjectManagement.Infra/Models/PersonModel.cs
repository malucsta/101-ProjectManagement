using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Infra.Models;

[Table("person")]
public class PersonModel
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
