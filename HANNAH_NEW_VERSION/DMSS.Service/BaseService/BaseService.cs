using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq.Expressions;

namespace SERVICE.BaseService
{
    public class BaseService<Modal, ViewModal> : IBaseService<Modal, ViewModal> where Modal : class
                                                                          where ViewModal : class
    {
        private readonly IBaseRepository<Modal> _repository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<Modal> repository,
            IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public virtual List<ViewModal> GetAll()
        {
            var results = _repository.GetAll().ToList();
            return _mapper.Map<List<Modal>, List<ViewModal>>(results);
        }
        public virtual List<Modal> GetAll(Expression<Func<Modal, bool>> predicate)
        {
            var results = _repository.GetAll(predicate).ToList();
            return results;
        }

        public virtual List<Modal> GetAllPaging(Expression<Func<Modal, bool>> predicate, Expression<Func<Modal, int>> oderby, int Take, int Skip)
        {
            var results = _repository.GetAllPaging(predicate, oderby, Take, Skip).ToList();
            return results;
        }

    }
}
