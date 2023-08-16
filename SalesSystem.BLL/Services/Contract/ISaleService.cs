using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesSystem.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesSystem.BLL.Services.Contract
{
    public interface ISaleService
    {
        Task<SaleDTO> Register(SaleDTO model);
        Task<List<SaleDTO>> Record(string ToLookFor, string SaleNumber, string StartDate, string EndingDate);
        Task<List<ReportDTO>> Report(string StartDate, string EndingDate);
    }
}
