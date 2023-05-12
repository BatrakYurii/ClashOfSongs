using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Models.Parameters
{
    public class FilterModel
    {
        public string? SearchText { get; set; }

        public int[]? Sizes { get; set; }
    }
}
