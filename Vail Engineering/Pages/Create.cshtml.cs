using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vail_Engineering.Data;
using Vail_Engineering.Models;

namespace Vail_Engineering.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Vail_EngineeringContext _context;

        public CreateModel(Vail_EngineeringContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Record Record { get; set; } = default!;
        [BindProperty]
        public Outlets Outlet { get; set; }
        [BindProperty]
        public BinTypes BinType { get; set; }
        [BindProperty]
        public int LocationId { get; set; }
        [BindProperty]
        public int WasteCategoryId { get; set; }
        public IList<WasteCategory> WasteCategories { get; set; }
        public IList<Location> Locations { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.WasteChapter != null)
            {
                WasteCategories = await _context.WasteCategory.ToListAsync();
            }
            if (_context.Location != null)
            {
                Locations = await _context.Location.ToListAsync();
            }

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || _context.Record == null || Record == null)
            //{
            //    return Page();
            //}

            Record.Date = Record.Date.Date;

            Record.Outlet = Outlet;
            Record.Bin.BinType = BinType;
            //Record.Bin = new Bin
            //{
            //    Quantity = Record.Bin.Quantity
            //};
            Record.Location = await _context.Location.FirstAsync(m => m.Id == LocationId);

            Record.WasteCategory = await _context.WasteCategory.FirstAsync(m => m.Id == WasteCategoryId);

            _context.Record.Add(Record);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
