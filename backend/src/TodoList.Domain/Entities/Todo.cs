namespace TodoList.Domain.Entities;

public class Todo
{
    private const int MaxTitleLength = 100;
    private const int MaxDescriptionLength = 255;

    public Guid Id { get;}
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get;}


    public Todo(Guid id, string title, string? description, DateTime createdAt)
    {
        //validate id
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty", nameof(id));

        //validate title
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required", nameof(title));

        title = title.Trim();
        if (title.Length > MaxTitleLength)
            throw new ArgumentException($"Title cannot exceed {MaxTitleLength} characters.", nameof(title));

        //validate description
        description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();

        if (description is not null && description.Length > MaxDescriptionLength)
            throw new ArgumentException($"Description cannot exceed {MaxDescriptionLength} characters.",
                nameof(description));


        Id = id;
        Title = title;
        Description = description;
        CreatedAt = createdAt;
    }
}