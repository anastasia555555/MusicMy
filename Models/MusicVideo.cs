using System.ComponentModel.DataAnnotations;

namespace MusicMy.Models
{
    public class MusicVideo
    {
        public int Id { get; set; }

        public string OriginalId { get; set; }

        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        [StringLength(50)]
        [Required]
        public string Channel { get; set; } 

        public string ThumbnailURL { get; set; } 

        [Required]
        public string URL { get; set; }

        [StringLength(50)]
        public string Playlist { get; set; }
    }
}