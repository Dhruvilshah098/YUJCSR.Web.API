using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Domain.Response;

namespace YUJCSR.Infrastructure.Repositories.Interface
{
    public interface IRepositoryBase<T> 
    {
        RepositoryResponse Save(T obj);
        RepositoryResponse Update(T obj);
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        RepositoryResponse GetByCondition(Expression<Func<T, bool>> expression);
        List<T> FindAll();
        T FindById(Expression<Func<T, bool>> expression);
        RepositoryResponse UpdateColumns(T obj, params Expression<Func<T, object>>[] properties);
        object ExecWithStoreProcedure(string query, params object[] parameters);
    }
}
