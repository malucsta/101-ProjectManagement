namespace ProjectManagement.Core.Cards;

public class Card
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Ranking { get; set; }
    public Guid ListId { get; set; }
    public Guid PersonId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime Date { get; set; }
}
