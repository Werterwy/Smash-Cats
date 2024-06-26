﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Smash.Cats.API.Models
{
    public partial class User
    {
        // [Key]
        public int Id { get; set; }
       // [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Коллекция достижений
        // public List<Achievement> Achievements { get; set; } = new List<Achievement>();
    }

    public class Achievement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
