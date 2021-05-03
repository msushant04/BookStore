using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class BookGallary
    {
        public int Id { get; set; }
        public int bookId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Books Book { get; set; }
    }
}
