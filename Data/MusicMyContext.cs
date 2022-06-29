using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicMy.Models;

namespace MusicMy.Data
{
    public class MusicMyContext : DbContext
    {
        public MusicMyContext (DbContextOptions<MusicMyContext> options)
            : base(options)
        {
        }

        public DbSet<MusicMy.Models.MusicVideo> MusicVideo { get; set; }
    }
}
