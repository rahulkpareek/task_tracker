using System.Text.Json.Serialization;

public class Task
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("status")]
    public TaskStatus Status { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    public Task()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Status = TaskStatus.Todo;
    }
}

public enum TaskStatus
{
    Todo,
    InProgress,
    Done
}