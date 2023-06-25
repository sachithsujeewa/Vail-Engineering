using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vail_Engineering.Data;
using Vail_Engineering.Models;

namespace Vail_Engineering.Pages.wcat
{
    public class DeleteModel : PageModel
    {
        private readonly Vail_Engineering.Data.Vail_EngineeringContext _context;

        public DeleteModel(Vail_Engineering.Data.Vail_EngineeringContext context)
        {
            _context = context;
        }

        [BindProperty]
      public WasteCategory WasteCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.WasteCategory == null)
            {
                return NotFound();
            }

            var wastecategory = await _context.WasteCategory.FirstOrDefaultAsync(m => m.Id == id);

            if (wastecategory == null)
            {
                return NotFound();
            }
            else 
            {
                WasteCategory = wastecategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.WasteCategory == null)
            {
                return NotFound();
            }
            var wastecategory = await _context.WasteCategory.FindAsync(id);

            if (wastecategory != null)
            {
                WasteCategory = wastecategory;
                _context.WasteCategory.Remove(WasteCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
