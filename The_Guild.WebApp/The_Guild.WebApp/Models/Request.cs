using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.Models
{
    public class Request
    {
        private string _description, _requirements;
        private decimal? _reward, _cost;

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Rank ID")]
        public int? RankId { get; set; }

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

        [Display(Name = "Progress ID")]
        public int ProgressId { get; set; }

        public IEnumerable<AdventureParty> AdventureParty { get; set; }
        public IEnumerable<RequestingGroup> RequestingParty { get; set; }

        //for selecting requesting group members
        public List<Users> Requesters { get; set; } = new List<Users>();
    }
}
