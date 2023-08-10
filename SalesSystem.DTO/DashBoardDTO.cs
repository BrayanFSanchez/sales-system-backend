using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.DTO
{
    public class DashBoardDTO
    {
        public int TotalSales { get; set; }
        public string? TotalRevenues { get; set; }
        public List<WeekSaleDTO> LastWeekSales { get; set; }
    }
}
