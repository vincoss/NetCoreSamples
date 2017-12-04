using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tutorials_RazorPagesMovie.Models
{
    public class FileUpload
    {
        [Required]
        [Display(Name = "Title")]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Public Schedule")]
        public IFormFile UploadPublicSchedule { get; set; }

        [Required]
        [Display(Name = "Private Schedule")]
        public IFormFile UploadPrivateSchedule { get; set; }
    }
}
