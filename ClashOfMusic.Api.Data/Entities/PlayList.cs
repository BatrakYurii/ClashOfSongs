﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Entities
{
    public class PlayList
    {
        public int Id { get; set; }
        public  string? Title { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<string> PreviewImages { get; set; }
        public IEnumerable<PlayListsSongs> PlayListsSongs { get; set; }

    }
}
