using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;

namespace The_Guild.WebApp.ApiModels
{
    public class ApiLogin
    {
        private string _userName;
        private string _password;
        private bool _remember;

        public string Username {
            get => _userName;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _userName = value;
            }
        }


        [DataType(DataType.Password)]
        public string Password {
            get => _password;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _password = value;
            }
        }
        public bool RememberMe {
            get => _remember;
            set
            {
                _remember = value;
            }
        }
    }
}
