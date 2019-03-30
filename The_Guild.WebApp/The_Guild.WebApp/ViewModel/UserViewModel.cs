using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using The_Guild.WebApp.ApiModels;
using The_Guild.WebApp.Models;

namespace The_Guild.WebApp.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Range(0, 900000)]
        public decimal? Salary { get; set; }


        [Range(0, 40)]
        public int? Strength { get; set; }

        [Range(0, 40)]
        public int? Dex { get; set; }

        [Range(0, 40)]
        public int? Wisdom { get; set; }

        [Range(0, 40)]
        public int? Intelligence { get; set; }

        [Range(0, 40)]
        public int? Charisma { get; set; }

        [Range(0, 40)]
        public int? Constitution { get; set; }

        [Range(0, 900000)]
        [Display(Name = "Rank")]
        public int? RankId { get; set; }
        public ApiRanks Rank { get; set; }
        public IEnumerable<ApiRanks> Ranks { get; set; }

        public IEnumerable<Request> submittedRequests { get; set; }
        public IEnumerable<Request> acceptedRequests { get; set; }

        public UserViewModel() { }

        public UserViewModel(Users user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Salary = user.Salary;
            Strength = user.Strength;
            Dex = user.Dex;
            Wisdom = user.Wisdom;
            Intelligence = user.Intelligence;
            Charisma = user.Charisma;
            Constitution = user.Constitution;
            RankId = user.RankId;
            Rank = user.Rank;
            Ranks = user.Ranks;
        }

    }
}
