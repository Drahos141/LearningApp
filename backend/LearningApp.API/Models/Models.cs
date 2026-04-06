namespace LearningApp.API.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Description { get; set; } = "";
    public string Color { get; set; } = "";
    public List<Subcategory> Subcategories { get; set; } = new();
}

public class Subcategory
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<Lesson> Lessons { get; set; } = new();
}

public class Lesson
{
    public int Id { get; set; }
    public int SubcategoryId { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public int Order { get; set; }
}

public class Quiz
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public List<Question> Questions { get; set; } = new();
}

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public List<string> Options { get; set; } = new();
    public int CorrectIndex { get; set; }
    public string Explanation { get; set; } = "";
}

public enum MiniGameType { Flashcard, Matching }

public class MiniGame
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public MiniGameType Type { get; set; }
    public List<MiniGameItem> Items { get; set; } = new();
}

public class MiniGameItem
{
    public string Term { get; set; } = "";
    public string Definition { get; set; } = "";
}
