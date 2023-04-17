using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TextEditor.Areas.Identity.Data;
using TextEditor.Models;

namespace TextEditor.Data;

public class TextEditorContext : IdentityDbContext<TextEditorUser>
{
    public TextEditorContext(DbContextOptions<TextEditorContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<TextEditor.Models.TextFileModel>? TextFileModel { get; set; }
}
