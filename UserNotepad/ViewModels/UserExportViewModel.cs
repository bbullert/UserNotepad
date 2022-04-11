using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotepad.ViewModels
{
    public class UserExportViewModel
    {
        [Required]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "File Type")]
        public string FileType { get; set; }

        public List<SelectListItem> FileTypeOptions { get; set; }
    }
}
