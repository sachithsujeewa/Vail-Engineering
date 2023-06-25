using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vail_Engineering.Data;
using Vail_Engineering.Models;

namespace Vail_Engineering.Pages.wcha
{
    public class DeleteModel : PageModel
    {
        private readonly Vail_Engineering.Data.Vail_EngineeringContext _context;

        public DeleteModel(Vail_Engineering.Data.Vail_EngineeringContext context)
        {
            _context = context;
        }

        [BindProperty]
      public WasteChapter WasteChapter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.WasteChapter == null)
            {
                return NotFound();
            }

            var wastechapter = await _context.WasteChapter.FirstOrDefaultAsync(m => m.Id == id);

            if (wastechapter == null)
            {
                return NotFound();
            }
            else 
            {
                WasteChapter = wastechapter;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.WasteChapter == null)
            {
                return NotFound();
            }
            var wastechapter = await _context.WasteChapter.FindAsync(id);

            if (wastechapter != null)
            {
                WasteChapter = wastechapter;
                _context.WasteChapter.Remove(WasteChapter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
