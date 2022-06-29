using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace MusicMy.Models
{
    public class BrowseViewModel
    {
        public IList<Video>? MusicVideos { get; set; }
        public IList<SearchResult> SearchResults { get; set; }
        public string? SearchString { get; set; }
    }
}