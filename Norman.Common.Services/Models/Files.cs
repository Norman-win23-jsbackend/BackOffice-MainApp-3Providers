using System.ComponentModel.DataAnnotations;

namespace Norman.Common.Services.Models
{
    public class Files
    {
        [Key]
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public string? ContentType { get; set; }
        public string? ContainerName { get; set; }
    }
}
