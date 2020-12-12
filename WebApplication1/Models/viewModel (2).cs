using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class viewModel
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "please enter your name")]
        //[StringLength(50, MinimumLength = 20, ErrorMessage = "Must be at most 20 characters long.")]

        public string Name { get; set; }
        [Required(ErrorMessage = "please enter your Address")]
        //[StringLength(50, MinimumLength = 20, ErrorMessage = "Must be at least 20 characters long.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "please enter your Mobile")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Must be at most 11 characters long.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "please enter your date of birth")]
       // [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string DateOfBirthstr { get; set; }
        [Required(ErrorMessage = "please enter your image")]
        public string imageName { get; set; }
        public int imageID { get; set; }
        [Required(ErrorMessage = "please select your images")]
        public string images { get; set; }
        [Required(ErrorMessage = "please select your images")]
        public List<string> imagess { get; set; }
        [Required(ErrorMessage = "please select your items")]
        public List<string> multipleSelected { get; set; }
        public virtual client client { get; set; }
        public List<clientType> Type { get; set; }
        [Required(ErrorMessage = "this field is reguired")]
        public string SummerNote { get; set; }
    }
}