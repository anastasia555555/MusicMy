using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MusicMy.Models
{
    public class MySavesViewModel
    {
        public List<MusicVideo>? MusicVideos { get; set; }
        public SelectList? Playlists { get; set; }
        public string? MusicVideoPlaylist { get; set; }
        public string? SearchString { get; set; }
    }
}