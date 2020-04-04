using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ExploreCalifornia.Models
{
    public class Post
    {
        public long Id { get; set; }

        private string _key;
        public string Key
        {
            get
            {
                if(_key == null)
                {
                    _key = Regex.Replace(Title.ToLower(CultureInfo.CurrentCulture), "[^a-z0-9]", "-");
                }
                return _key;
            }
            set { _key = value; }
        }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title mast be between 5 and 100 characters long")]
        [Display(Name = "Post Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        public string Author { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Post body should be more than 100 characters long")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public DateTime Posted { get; set; }
    }
}