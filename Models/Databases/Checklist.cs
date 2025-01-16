using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Checklist
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("checklistId")]
    public int ChecklistId { get; set; }

    [JsonPropertyName("checklistName")]
    public string ChecklistName { get; set; } = string.Empty;

    [JsonPropertyName("items")] public virtual ICollection<ChecklistItem> Items { get; set; } = [];
}

public class ChecklistItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("checklistItemId")]
    public int ChecklistItemId { get; set; }

    [JsonPropertyName("checklistItemName")]
    public string ChecklistItemName { get; set; } = string.Empty;

    [JsonPropertyName("checklistId")]
    public int ChecklistId { get; set; }

    [JsonPropertyName("done")]
    public bool Done { get; set; } = false;

    [ForeignKey("ChecklistId")]
    [JsonPropertyName("checklist")]
    public virtual Checklist Checklist { get; set; }
}