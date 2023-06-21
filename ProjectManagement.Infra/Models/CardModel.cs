using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Infra.Models;

[Table("card")]
public class CardModel
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Ranking { get; set; }

    [ForeignKey("list_id")]
    public Guid ListId { get; set; }

    [ForeignKey("person_id")]
    public Guid PersonId { get; set; }
    public bool IsCompleted { get; set; }

    [Column(TypeName = "date")]
    public DateTime Date { get; set; }


    public virtual PersonModel Person { get; set; }
    public virtual ListModel List { get; set; }

    
    public CardModel()
    {
        Person = new PersonModel();
        List = new ListModel();
    }

}
