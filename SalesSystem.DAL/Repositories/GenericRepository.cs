using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesSystem.DAL.Repositories.Contract;
using SalesSystem.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SalesSystem.DAL.Repositories
{
    public class GenericRepository<TModel>: IGenericRepository<TModel> where TModel : class
    {
        private readonly DbsaleContext _dbcontext;

        public GenericRepository(DbsaleContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel model = await _dbcontext.Set<TModel>().FirstOrDefaultAsync(filter);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Add(model);
                await _dbcontext.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Edit(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Update(model);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Remove(model);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IQueryable<TModel>> Consult(Expression<Func<TModel, bool>> filter = null)
        {
            try
            {
                IQueryable<TModel> queryModel = filter == null ? _dbcontext.Set<TModel>() : _dbcontext.Set<TModel>().Where(filter);
                return await Task.FromResult(queryModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
