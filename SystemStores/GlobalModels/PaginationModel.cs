using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStores.GlobalModels
{
    public class PaginationModel:BoostrapPopModal
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderingBy { get; set; }
        public string OrderingDirection { get; set; }
    }
}
