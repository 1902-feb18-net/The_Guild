using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using The_Guild.WebApp.Models;

namespace The_Guild.WebApp.ViewModel
{
    public class UserIndexModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Rank")]
        public int? RankId { get; set; }
        public Rank Rank { get; set; }
        public List<ApiModels.ApiRanks> Ranks { get; set; }

    }
}
