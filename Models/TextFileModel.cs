using TextEditor.Areas.Identity.Data;

namespace TextEditor.Models
{
    public class TextFileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set;}

        public int UserId { get; set; }

    }
}
