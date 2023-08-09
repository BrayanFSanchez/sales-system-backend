using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesSystem.DAL.DBContext;
using SalesSystem.DAL.Repositories.Contract;
using SalesSystem.Model;

namespace SalesSystem.DAL.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly DbsaleContext _dbContext;

        public SaleRepository(DbsaleContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Sale> Register(Sale model)
        {
            Sale saleGenerated = new Sale();

            using (var transaction = _dbContext.Database.BeginTransaction()) {
                try
                {
                    foreach (SaleDetail sd in model.SaleDetail)
                    {
                        Product productFound = _dbContext.Products.Where(p => p.IdProduct == sd.IdProduct).First();

                        productFound.Stock = productFound.Stock - sd.Amount;
                        _dbContext.Products.Update(productFound);
                    }
                    await _dbContext.SaveChangesAsync();

                    DocumentNumber correlative = _dbContext.DocumentNumbers.First();

                    correlative.LastNumber = correlative.LastNumber + 1;
                    correlative.RegistrationDate = DateTime.Now;

                    _dbContext.DocumentNumbers.Update(correlative);
                    await _dbContext.SaveChangesAsync();

                    int numberDigits = 4;
                    string zeros = string.Concat(Enumerable.Repeat("0", numberDigits));
                    string saleNumber = zeros + correlative.LastNumber.ToString();

                    saleNumber = saleNumber.Substring(saleNumber.Length - numberDigits, numberDigits);

                    model.DocumentNumber = saleNumber;

                    await _dbContext.Sales.AddAsync(model);
                    await _dbContext.SaveChangesAsync();

                    saleGenerated = model;

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                return saleGenerated;
            }
        }
    }
}
