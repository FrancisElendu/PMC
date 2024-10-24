using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMC.Application.Common
{
    public interface ISortableQuery
    {
        string SortColumn { get; set; }
        string SortDirection { get; set; } // "asc" or "desc"
    }
}
