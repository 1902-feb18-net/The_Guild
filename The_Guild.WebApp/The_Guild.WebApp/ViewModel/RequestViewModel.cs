using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using The_Guild.WebApp.Models;

namespace The_Guild.WebApp.ViewModel
{
    public class RequestViewModel
    {
        private string _description, _requirements;
        private decimal? _reward, _cost;

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Descript
        {
            get => _description;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _description = value;
            }
        }

        [Required]
        public string Requirements
        {
            get => _requirements;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _requirements = value;
            }
        }

        [Range(0.00, 900000.00)]
        public decimal? Reward
        {
            get => _reward;
            set
            {
                if (CheckConstraints.NonNegativeDecimal(value))
                {
                    _reward = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [Range(0.00, 900000.00)]
        public decimal? Cost
        {
            get => _cost;
            set
            {
                if (CheckConstraints.NonNegativeDecimal(value))
                {
                    _cost = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int RankId { get; set; }
        public int ProgressId { get; set; }

        [Display(Name = "Rank")]
        public Ranks Rank { get; set; } //to display rank name
        [Display(Name = "Progress")]
        public Progress Progress { get; set; } //to display progress name

        //List of all ranks and progresses to choose from.
        //to be displayed as an html select (dropdown list) 
        public List<Ranks> ranks { get; set; }
        public List<Progress> progresses { get; set; }

        //list of all? users (want to filter by customer role) to select requesters
        [Display(Name = "Requesting Group Members")]
        public List<RequesterViewModel> requesters { get; set; } = new List<RequesterViewModel>();


        public RequestViewModel() { }

        public RequestViewModel(Request req)
        {
            Id = req.Id;
            Descript = req.Descript;
            Requirements = req.Requirements;
            Reward = req.Reward;
            Cost = req.Cost;
        }
    }
}
