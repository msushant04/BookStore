using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Enum;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookModel
    {
        //[Display(Name ="Custom Email")]
        //[DataType(DataType.EmailAddress)]
        //public string MyField { get; set; }
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

        //[Required]
        //public LanguageEnum LanguageEnum { get; set; }
        //public List<string> MultiLanguage { get; set; }
    }
}
