﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Parameters
{
    public class Pagination
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int PageCount { get; set; }
    }
}
