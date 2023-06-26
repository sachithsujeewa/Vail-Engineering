using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vail_Engineering.Data;
using Vail_Engineering.Models;

namespace Vail_Engineering.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Vail_Engineering.Data.Vail_EngineeringContext _context;

        public IndexModel(Vail_Engineering.Data.Vail_EngineeringContext context)
        {
            _context = context;
        }

        public IList<Record> Record { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Record != null)
            {
                Record = await _context.Record
                    .Include(r => r.Bin)
                    .Include(r => r.WasteChapter).ThenInclude(c=>c.Category)
                    .ToListAsync();
            }
        }
    }
}
