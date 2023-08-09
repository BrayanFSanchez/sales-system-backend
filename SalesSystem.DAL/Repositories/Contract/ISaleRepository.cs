using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesSystem.Model;

namespace SalesSystem.DAL.Repositories.Contract
{
    public interface ISaleRepository: IGenericRepository<Sale>
    {
        Task<Sale> Register(Sale model);
    }
}
