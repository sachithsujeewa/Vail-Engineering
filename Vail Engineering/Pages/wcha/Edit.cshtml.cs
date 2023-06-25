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
    public class EditModel : PageModel
    {
        private readonly Vail_Engineering.Data.Vail_EngineeringContext _context;

        public EditModel(Vail_Engineering.Data.Vail_EngineeringContext context)
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

            var wastechapter =  await _context.WasteChapter.FirstOrDefaultAsync(m => m.Id == id);
            if (wastechapter == null)
            {
                return NotFound();
            }
            WasteChapter = wastechapter;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WasteChapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WasteChapterExists(WasteChapter.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WasteChapterExists(int id)
        {
          return (_context.WasteChapter?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
