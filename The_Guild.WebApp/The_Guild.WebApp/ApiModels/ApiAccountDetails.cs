﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp.ApiModels
{
    public class ApiAccountDetails
    {
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public int UserId { get; set; }
    }
}
