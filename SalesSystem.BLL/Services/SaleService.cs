using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Contract;
using SalesSystem.DAL.Repositories.Contract;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class SaleService: ISaleService
    {
        private ISaleRepository _saleRepository;
        private readonly IGenericRepository<SaleDetail> _saleDetailRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IGenericRepository<SaleDetail> saleDetailRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _mapper = mapper;
        }

        public async Task<SaleDTO> Register(SaleDTO model)
        {
            try
            {
                var saleGenerated = await _saleRepository.Register(_mapper.Map<Sale>(model));

                if(saleGenerated.IdSale == 0)
                {
                    throw new TaskCanceledException("Could not create");
                }

                return _mapper.Map<SaleDTO>(saleGenerated);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SaleDTO>> Record(string ToLookFor, string SaleNumber, string StartDate, string EndingDate)
        {
            IQueryable<Sale> query = await _saleRepository.Consult();
            var ResultList = new List<Sale>();

            try
            {
                if(ToLookFor == "date")
                {
                    DateTime date_Start = DateTime.ParseExact(StartDate, "dd/MM/yyyy", new CultureInfo("es-HN"));
                    DateTime date_End = DateTime.ParseExact(EndingDate, "dd/MM/yyyy", new CultureInfo("es-HN"));

                    ResultList = await query.Where(v =>
                        v.RegistrationDate.Value.Date >= date_Start.Date &&
                        v.RegistrationDate.Value.Date <= date_End.Date
                    ).Include(sd => sd.SaleDetail)
                    .ThenInclude(P => P.IdProductNavigation)
                    .ToListAsync();
                }
                else
                {
                    ResultList = await query.Where(v => v.DocumentNumber == SaleNumber
                    ).Include(sd => sd.SaleDetail)
                    .ThenInclude(P => P.IdProductNavigation)
                    .ToListAsync();
                }
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<SaleDTO>>(ResultList);
        }

        public async Task<List<ReportDTO>> Report(string StartDate, string EndingDate)
        {
            IQueryable<SaleDetail> query = await _saleDetailRepository.Consult();
            var ResultList = new List<SaleDetail>();

            try
            {
                DateTime date_Start = DateTime.ParseExact(StartDate, "dd/MM/yyyy", new CultureInfo("es-HN"));
                DateTime date_End = DateTime.ParseExact(EndingDate, "dd/MM/yyyy", new CultureInfo("es-HN"));

                ResultList = await query.Include(p => p.IdProductNavigation)
                    .Include(v => v.IdSaleNavigation)
                    .Where(sd => 
                        sd.IdSaleNavigation.RegistrationDate.Value.Date >= date_Start.Date &&
                        sd.IdSaleNavigation.RegistrationDate.Value.Date <= date_End.Date
                    ).ToListAsync();
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReportDTO>>(ResultList);
        }
    }
}
