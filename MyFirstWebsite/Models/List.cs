﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstWebsite.Models
{
    public class List
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Details { get; set; }

        public string Date_Posted { get; set; }
        public string Time_Posted { get; set; }
        public string Date_Edited { get; set; }
        public string Time_Edited { get; set; }
        public string Public { get; set; }
        public bool Check_Public { get; set; }
        public int User_Id { get; set; }
    }
}