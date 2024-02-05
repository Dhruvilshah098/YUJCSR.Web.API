using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using YUJCSR.Common.Constants;
using YUJCSR.Domain.Response;
using YUJCSR.Infrastructure.Repositories.Interface;

namespace YUJCSR.Infrastructure.Repositories.Implementation
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private YUJCSRContext _dbContext;
        private DbSet<T> dbSet;
        private readonly string _className;
        private readonly ILogger<RepositoryBase<T>> _logger;
        private RepositoryResponse _response;

        public RepositoryBase(YUJCSRContext dbcontext, ILogger<RepositoryBase<T>> logger)
        {
            _dbContext = dbcontext;
            dbSet = _dbContext.Set<T>();
            _logger = logger;
            _response = new RepositoryResponse();
        }

        public List<T> FindAll()
        {
            try
            {
                _logger.LogDebug(_className + " : FindAll started");
                var list = this._dbContext.Set<T>().ToList();
                _logger.LogDebug(_className + " : FindAll returned data");
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(_className + " : Error in FindAll ", ex);
                return null;
            }
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            try
            {
                _logger.LogDebug(_className + " : FindByCondition started");
                var list = _dbContext.Set<T>().Where(expression);
                _logger.LogDebug(_className + " : FindByCondition returned data");
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(_className + " : Error in FindByCondition ", ex);
                return null;
            }
        }
        public RepositoryResponse GetByCondition(Expression<Func<T, bool>> expression)
        {
            try
            {
                _logger.LogDebug(_className + " : GetByCondition started");
                var list = _dbContext.Set<T>().Where(expression).ToList();
                _logger.LogDebug(_className + " : GetByCondition returned data");
                _response.Status = true;
                _response.Message = SuccessMessage.Save_Message;
                _response.dataList = list;
                return _response;
            }
            catch (Exception ex)
            {
                _logger.LogError(_className + " : Error in FindByCondition ", ex);
                _response.Status = false;
                _response.Message = ErrorMessage.GenericError_Message;

                return _response;
            }
        }

        public T FindById(Expression<Func<T, bool>> expression)
        {
            try
            {



                _logger.LogDebug(_className + " : FindById started");
                var obj = _dbContext.Set<T>().FirstOrDefault(expression);
                _logger.LogDebug(_className + " : FindById returned data");
                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogError(_className + " : Error in FindById ", ex);
                return null;
            }
        }
        public object ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _dbContext.Set<T>().FromSqlRaw(query, parameters).ToList();

        }
      
        public RepositoryResponse Save(T obj)
        {
            _logger.LogDebug(_className + " : Insert started");
            try
            {
                dbSet.Add(obj);
                _logger.LogDebug(_className + " : dbSet object added");
                _dbContext.SaveChanges();
                _logger.LogDebug(_className + " : dbContext savechanges");
                _response.Status = true;
                _response.Message = SuccessMessage.Save_Message;
                _response.data = obj;
            }
            catch (DbUpdateException exDbUpdate)
            {
                _logger.LogError(_className + " : Error in Save ", exDbUpdate);
                _response.Status = false;
                _response.Message = ErrorMessage.Save_Message;
                _response.Error = new ErrorResponse(500, exDbUpdate.InnerException.ToString(), ErrorType.DBError);
            }

            catch (Exception ex)
            {
                _logger.LogError(_className + " : Error in Save ", ex);
                _response.Status = false;
                _response.Message = ErrorMessage.Save_Message;
                _response.Error = new ErrorResponse(500, ex.InnerException.ToString(), ErrorType.DBError);
            }
            _logger.LogDebug(_className + " : Insert completed");
            return _response;
        }

        public RepositoryResponse Update(T obj)
        {
            _logger.LogDebug(_className + " : Update started");
            try
            {
                dbSet.Attach(obj);
                _dbContext.Entry(obj).State = EntityState.Modified;
                _dbContext.SaveChanges();
                _logger.LogDebug(_className + " : dbSet object updated");
                _response.Status = true;
                _response.Message = SuccessMessage.Update_Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(_className + " : Error in Update ", ex);
                _response.Status = false;
                _response.Message = ErrorMessage.Update_Message;
                _response.Error = new ErrorResponse(500, ex.Message, ErrorType.DBError);
            }

            _logger.LogDebug(_className + " : Update completed");
            return _response;
        }

        public RepositoryResponse UpdateColumns(T obj, params Expression<Func<T, object>>[] properties)
        {
            _logger.LogDebug(_className + " : Update started");
            try
            {

                _dbContext.Entry(obj).State = EntityState.Modified;
                foreach (var property in properties)
                {
                    var propertyName = ExpressionHelper.GetExpressionText(property);
                    _dbContext.Entry(obj).Property(propertyName).IsModified = true;
                }
                //  _dbContext.Set<T>().Update(obj);
                _dbContext.SaveChanges();
                _logger.LogDebug(_className + " : dbSet object updated");
                _response.Status = true;
                _response.Message = SuccessMessage.Update_Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(_className + " : Error in Update ", ex);
                _response.Status = false;
                _response.Message = ErrorMessage.Update_Message;
                _response.Error = new ErrorResponse(500, ex.InnerException.ToString(), ErrorType.DBError);
            }

            _logger.LogDebug(_className + " : Update completed");
            return _response;
        }
    }
}
