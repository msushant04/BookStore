using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class LanguageRepository
    {
        private readonly BookStoreContext _context = null;
        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<LanguageModel>> Languages()
        {
            var lang = await _context.Language.Select(x => new LanguageModel()
            {
                Id = x.Id,
                Text = x.Text,
                Description = x.Description
            }).ToListAsync();
            return lang;
        }
    }
}
