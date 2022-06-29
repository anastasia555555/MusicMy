using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicMy.Data;
using MusicMy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net;


namespace MusicMy.Controllers
{
    public class MusicVideosController : Controller
    {
        private readonly MusicMyContext _context;

        public MusicVideosController(MusicMyContext context)
        {
            _context = context;
        }

        //GET: MusicVideos/Browse/{searchString?}
        public async Task<IActionResult> Browse(string searchString)
        {
            var service = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "AIzaSyCNXFOiKv6FMvU5vWL0DtOOsnOa3_RplkA",

                //AIzaSyCNXFOiKv6FMvU5vWL0DtOOsnOa3_RplkA  Hanna
                //AIzaSyDHU9hgIvte52Jb8sTX_b09V6jj5X2J8zI  Me

                ApplicationName = this.GetType().ToString()
            });

            var BrowseVM = new BrowseViewModel();

            if (!string.IsNullOrEmpty(searchString))
            {
                var searchRequest = service.Search.List("id,snippet");
                searchRequest.Q = searchString;
                searchRequest.Type = "video";
                searchRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
                searchRequest.MaxResults = 50;

                var results = await searchRequest.ExecuteAsync();
                BrowseVM.SearchResults = results.Items;
            }
            else
            {
                var request = service.Videos.List("snippet,player");
                request.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
                request.VideoCategoryId = "10"; //https://www.googleapis.com/youtube/v3/videoCategories?part=snippet&regionCode=us&key=AIzaSyDHU9hgIvte52Jb8sTX_b09V6jj5X2J8zI
                request.MaxResults = 50;

                var results = await request.ExecuteAsync();

                BrowseVM.MusicVideos = results.Items;
            }

            return View(BrowseVM);
        }

        // GET: MusicVideos/MySaves
        public async Task<IActionResult> MySaves(string musicVideoPlaylist, string searchString)
        {
            IQueryable<string> playlistQuery = from m in _context.MusicVideo
                                            orderby m.Playlist
                                            select m.Playlist;
            var movies = from m in _context.MusicVideo
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString) 
                                        || s.Channel!.Contains(searchString)
                                        || s.Playlist!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(musicVideoPlaylist))
            {
                movies = movies.Where(x => x.Playlist == musicVideoPlaylist);
            }

            var MusicVideosPlaylistVM = new MySavesViewModel
            {
                Playlists = new SelectList(await playlistQuery.Distinct().ToListAsync()),
                MusicVideos = await movies.ToListAsync()
            };

            return View(MusicVideosPlaylistVM);
        }

        // GET: MusicVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicVideo = await _context.MusicVideo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicVideo == null)
            {
                return NotFound();
            }

            return View(musicVideo);
        }

        // POST: MusicVideos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OriginalId,Title,Channel,URL,ThumbnailURL,Playlist")] MusicVideo musicVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MySaves));
            }
            return View(musicVideo);
        }

        // GET: MusicVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicVideo = await _context.MusicVideo.FindAsync(id);
            if (musicVideo == null)
            {
                return NotFound();
            }

            return View(musicVideo);
        }

        // POST: MusicVideos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OriginalId,Title,Channel,URL,ThumbnailURL,Playlist")] MusicVideo musicVideo)
        {
            if (id != musicVideo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicVideoExists(musicVideo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MySaves));
            }
            return View(musicVideo);
        }

        // GET: MusicVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicVideo = await _context.MusicVideo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicVideo == null)
            {
                return NotFound();
            }

            return View(musicVideo);
        }

        // POST: MusicVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicVideo = await _context.MusicVideo.FindAsync(id);
            _context.MusicVideo.Remove(musicVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MySaves));
        }

        private bool MusicVideoExists(int id)
        {
            return _context.MusicVideo.Any(e => e.Id == id);
        }
    }
}
