using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vail_Engineering.Data;
using Vail_Engineering.Models;

namespace Vail_Engineering.Pages.wcha
{
    public class CreateModel : PageModel
    {
        private readonly Vail_Engineering.Data.Vail_EngineeringContext _context;

        public List<WasteCategory> Categories { get; set; }

        public CreateModel(Vail_Engineering.Data.Vail_EngineeringContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.WasteChapter != null)
            {
                Categories = await _context.WasteCategory.ToListAsync();
            }

            return Page();
        }

        [BindProperty]
        public WasteChapter WasteChapter { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            WasteChapter.Category = await _context.WasteCategory.FirstOrDefaultAsync(m => m.Id == WasteChapter.Category.Id);
            _context.WasteChapter.Add(WasteChapter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
