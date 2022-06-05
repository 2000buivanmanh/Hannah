using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.BaseService
{
    public interface IBaseService<Modal, ViewModal> where Modal : class where ViewModal : class
    {
        List<ViewModal> GetAll();
        List<Modal> GetAll(Expression<Func<Modal, bool>> predicate);
        List<Modal> GetAllPaging(Expression<Func<Modal, bool>> predicatem, Expression<Func<Modal, int>> oderby, int Take, int Skip);
    }
}
