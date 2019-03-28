using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using The_Guild.WebApp.Models;

namespace The_Guild.WebApp.ViewModel
{
    public class RequestViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Descript { get; set; }

        [Required]
        public string Requirements { get; set; }

        [Range(0.00, 900000.00)]
        public decimal? Reward { get; set; }

        [Range(0.00, 900000.00)]
        public decimal? Cost { get; set; }

        [Display(Name = "Progress ID")]
        public int? ProgressId { get; set; }

        [Display(Name = "Rank ID")]
        public int? RankId { get; set; }

        //public Ranks Rank { get; set; } //to display rank name
        //public Progress Progress { get; set; } //to display progress name

        //List of all ranks and progresses to choose from.
        //to be displayed as an html select (dropdown list) 
        public List<Ranks> ranks { get; set; }
        public List<Progress> progresses { get; set; }

        //list of all? users (want to filter by customer role) to select requesters
        [Display(Name = "Requesting Group Members")]
        public List<RequesterViewModel> requesters { get; set; } = new List<RequesterViewModel>();
    }
}
