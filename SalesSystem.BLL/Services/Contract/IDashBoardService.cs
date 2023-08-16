using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Contract
{
    public interface IDashBoardService
    {
        Task<DashBoardDTO> Summary();
    }
}
