using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SalesSystem.BLL.Services.Contract;
using SalesSystem.DAL.Repositories.Contract;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> List()
        {
            try
            {
                var categoryList = await _categoryRepository.Consult();

                return _mapper.Map<List<CategoryDTO>>(categoryList.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
