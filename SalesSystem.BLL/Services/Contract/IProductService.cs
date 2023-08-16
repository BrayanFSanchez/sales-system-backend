using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesSystem.DTO;

namespace SalesSystem.BLL.Services.Contract
{
    public interface IProductService
    {
        Task<List<ProductDTO>> List();
        Task<ProductDTO> Create(ProductDTO model);
        Task<bool> Edit(ProductDTO model);
        Task<bool> Delete(int id);
    }
}
