using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Contract;
using SalesSystem.DAL.Repositories.Contract;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class ProductService: IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> List()
        {
            try
            {
                var queryProduct = await _productRepository.Consult();

                var productList = queryProduct.Include(cat => cat.IdCategorieNavigation).ToList();

                return _mapper.Map<List<ProductDTO>>(productList.ToList());
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductDTO> Create(ProductDTO model)
        {
            try
            {
                var productCreated = await _productRepository.Create(_mapper.Map<Product>(model));

                if(productCreated.IdProduct == 0)
                {
                    throw new TaskCanceledException("Could not create");
                }

                return _mapper.Map<ProductDTO>(productCreated);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Edit(ProductDTO model)
        {
            try
            {
                var productModel = _mapper.Map<Product>(model);

                var productFound = await _productRepository.Get(u =>
                        u.IdProduct == productModel.IdProduct
                    );

                if (productFound == null)
                {
                    throw new TaskCanceledException("The product does not exist");
                }

                productFound.Name = productModel.Name;
                productFound.IdCategorie = productModel.IdCategorie;
                productFound.Stock = productModel.Stock;
                productFound.Price = productModel.Price;
                productFound.IsActive = productModel.IsActive;

                bool response = await _productRepository.Edit(productFound);

                if (!response)
                {
                    throw new TaskCanceledException("Could not edit");
                }

                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var productFound = await _productRepository.Get(p => p.IdProduct == id);

                if (productFound == null)
                {
                    throw new TaskCanceledException("The product does not exist");
                }

                bool response = await _productRepository.Delete(productFound);

                if (!response)
                {
                    throw new TaskCanceledException("Could not delete");
                }

                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
