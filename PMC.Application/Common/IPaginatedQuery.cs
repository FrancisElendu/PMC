﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Common
{
    public interface IPaginatedQuery
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
