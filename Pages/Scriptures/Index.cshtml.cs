using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Models.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Models.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; }
       
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList BookList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Books { get; set; }
        public string DateSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            // Use LINQ to get list of books.
            IQueryable<string> bookQuery = from m in _context.Scripture
                                            orderby m.Book
                                            select m.Book;

            IQueryable<Scripture> dateQuery = from s in _context.Scripture select s;
                                           

            var notes = from m in _context.Scripture
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                notes = notes.Where(s => s.Note.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(Books))
            {
                notes = notes.Where(x => x.Book == Books);
            }


            switch (sortOrder)
            {
                
                case "Date":
                    dateQuery = dateQuery.OrderBy(s => s.NoteDate);
                    break;
                default:
                    dateQuery = dateQuery.OrderByDescending(s => s.NoteDate);
                    break;
            }



            BookList = new SelectList(await bookQuery.Distinct().ToListAsync());
            if (sortOrder == "Date" || sortOrder == "date_desc")
            {
                Scripture = await dateQuery.AsNoTracking().ToListAsync();
            }else Scripture = await notes.ToListAsync();
            
        }
    }
}
