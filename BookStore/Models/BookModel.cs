using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Enum;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100,MinimumLength =5)]
        [Required(ErrorMessage ="Please enter Book Name.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter Auther Name.")]
        public string Auther { get; set; }
        [Required]
        public int? Price { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Path { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please enter Total Pages.")]
        [Display(Name ="Total Book Pages")]
        public int? Pages { get; set; }
        [Required]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please select cover photo.")]
        public IFormFile CoverPhoto { get; set; }
        [Required(ErrorMessage = "Please select cover Gallary.")]
        public IFormFileCollection GallaryFiles { get; set; }
        public List<GallaryModel> Gallary { get; set; }
        [Required(ErrorMessage = "Please Upload PDF.")]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }

    }
}
