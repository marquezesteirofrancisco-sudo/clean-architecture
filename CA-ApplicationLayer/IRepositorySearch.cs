using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public interface IRepositorySearch<TMode, TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TMode, bool>> predicate);

    }
}
