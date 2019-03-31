using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.ApiModels
{
    public class ApiUsers
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


        public string FirstName
        {
            get => _first;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _first = value;
            }

        }


        public string LastName
        {
            get => _last;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _last = value;
            }
        }


        public decimal? Salary
        {
            get => _sal;
            set
            {
                if (!(value < 0) && value <= 900000)
                {
                    _sal = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }


        public int? Strength
        {
            get => _str;
            set
            {
                if (!(value < 0) && value <= 40)
                {
                    _str = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int? Dex
        {
            get => _dex;
            set
            {
                if (!(value < 0) && value <= 40)
                {
                    _dex = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int? Wisdom
        {
            get => _wis;
            set
            {
                if (!(value < 0) && value <= 40)
                {
                    _wis = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }


        public int? Intelligence
        {
            get => _int;
            set
            {
                if (!(value < 0) && value <= 40)
                {
                    _int = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }


        public int? Charisma
        {
            get => _cha;
            set
            {
                if (!(value < 0) && value <= 40)
                {
                    _cha = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }


        public int? Constitution
        {
            get => _con;
            set
            {
                if (!(value < 0) && value <= 40)
                {
                    _con = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int? RankId { get; set; }
    }
}
