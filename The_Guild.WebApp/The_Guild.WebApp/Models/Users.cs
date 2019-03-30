using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using The_Guild.WebApp.ApiModels;

namespace The_Guild.WebApp.Models
{
    public class Users
    {
        private string _first,
                       _last;
        private decimal? _sal;
        private int? _str,
                     _dex,
                     _wis,
                     _int,
                     _cha,
                     _con;

        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName
        {
            get => _first;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _first = value;
            }
        }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName
        {
            get => _last;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _last = value;
            }
        }


        [Range(0, 900000)]
        public decimal? Salary
        {
            get => _sal;
            set
            {
                if (CheckConstraints.NonNegativeDecimal(value))
                {
                    _sal = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }


        [Range(0, 40)]
        public int? Strength
        {
            get => _str;
            set
            {
                if (CheckConstraints.ValidInt(value))
                {
                    _str = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [Range(0, 40)]
        public int? Dex
        {
            get => _dex;
            set
            {
                if (CheckConstraints.ValidInt(value))
                {
                    _dex = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [Range(0, 40)]
        public int? Wisdom
        {
            get => _wis;
            set
            {
                if (CheckConstraints.ValidInt(value))
                {
                    _wis = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [Range(0, 40)]
        public int? Intelligence
        {
            get => _int;
            set
            {
                if (CheckConstraints.ValidInt(value))
                {
                    _int = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [Range(0, 40)]
        public int? Charisma
        {
            get => _cha;
            set
            {
                if (CheckConstraints.ValidInt(value))
                {
                    _cha = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [Range(0, 40)]
        public int? Constitution
        {
            get => _con;
            set
            {
                if (CheckConstraints.ValidInt(value))
                {
                    _con = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [Range(0, 900000)]
        [Display(Name = "Rank")]
        public int? RankId { get; set; }
        public ApiRanks Rank { get; set; }
        public IEnumerable<ApiRanks> Ranks { get; set; }

        //public IEnumerable<AdventureParty> AdventureParty { get; set; }
        //public IEnumerable<RequestingParty> RequestingParty { get; set; }
    }
}
