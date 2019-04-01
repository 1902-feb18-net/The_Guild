using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using The_Guild.WebApp.ApiModels;
using The_Guild.WebApp.Models;

namespace The_Guild.WebApp.ViewModel
{
    public class QuesterViewModel
    {
        [Display(Name ="Quest\nDescription")]
        public string Quest { get; set; }

        [Display(Name ="Username")]
        public string Username { get; set; }

    }
}
