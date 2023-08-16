using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class DashBoardService : IDashBoardService
    {
        private ISaleRepository _saleRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public DashBoardService(ISaleRepository saleRepository, IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        private IQueryable<Sale> returnSales(IQueryable<Sale> saleTable, int subtractNumberDays) {
            DateTime? lastDate = saleTable.OrderByDescending(v => v.RegistrationDate).Select(v => v.RegistrationDate).First();

            lastDate = lastDate.Value.AddDays(subtractNumberDays);

            return saleTable.Where(v => v.RegistrationDate.Value.Date >= lastDate.Value.Date);
        }

        private async Task<int> TotalSalesLastWeek() {
            int total = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.Consult();

            if (_saleQuery.Count() > 0)
            {
                var saleTable = returnSales(_saleQuery, -7);
                total = saleTable.Count();
            }

            return total;
        }

        private async Task<string> TotalIncomeLastWeek(){
            decimal result = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.Consult();

            if(_saleQuery.Count() > 0)
            {
                var saleTable = returnSales(_saleQuery, -7);

                result = saleTable.Select(v => v.Total).Sum(v => v.Value);
            }

            return Convert.ToString(result,new CultureInfo("es-HN"));
        }

        private async Task<int> TotalProducts()
        {
            IQueryable<Product> _productQuery = await _productRepository.Consult();
            int total = _productQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> SalesLastWeek()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            IQueryable<Sale> _saleQuery =  await _saleRepository.Consult();

            if (_saleQuery.Count() > 0) {
                var saleTable = returnSales(_saleQuery, -7);

                result = saleTable
                    .GroupBy(v => v.RegistrationDate.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { date = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.date, elementSelector: r => r.total);
            }

            return result;
        }

        public async Task<DashBoardDTO> Summary()
        {
            DashBoardDTO vmDashBoard = new DashBoardDTO();

            try
            {
                vmDashBoard.TotalSales = await TotalSalesLastWeek();
                vmDashBoard.TotalRevenues = await TotalIncomeLastWeek();
                vmDashBoard.TotalProducts = await TotalProducts();

                List<WeekSaleDTO> weekSaleList = new List<WeekSaleDTO>();

                foreach (KeyValuePair<string,int> item in await SalesLastWeek())
                {
                    weekSaleList.Add(new WeekSaleDTO() { 
                        Date = item.Key,
                        Total = item.Value,
                    });
                }

                vmDashBoard.LastWeekSales = weekSaleList;
            }
            catch
            {
                throw;
            }

            return vmDashBoard;
        }
    }
}
