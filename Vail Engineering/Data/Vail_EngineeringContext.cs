using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vail_Engineering.Models;

namespace Vail_Engineering.Data
{
    public class Vail_EngineeringContext : DbContext
    {
        public Vail_EngineeringContext (DbContextOptions<Vail_EngineeringContext> options)
            : base(options)
        {
        }

        public DbSet<Vail_Engineering.Models.Record> Record { get; set; } = default!;

        public DbSet<Vail_Engineering.Models.WasteCategory> WasteCategory { get; set; } = default!;

        public DbSet<Vail_Engineering.Models.Location> Location { get; set; } = default!;

        public DbSet<Vail_Engineering.Models.WasteChapter> WasteChapter { get; set; } = default!;
    }
}
