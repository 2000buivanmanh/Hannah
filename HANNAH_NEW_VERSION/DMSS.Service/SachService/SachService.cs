using AutoMapper;
using DATA.Models;
using DATA.Repository;
using SERVICE.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class SachService : BaseService<Sach, Sach>, ISachService 
    {
        private readonly IBaseRepository<Sach> _baseRepository;
        private readonly IMapper _mapper;
        public SachService( IBaseRepository<Sach> baseRepository,
                            IMapper mapper) : base (baseRepository, mapper)
                            
        {
            _baseRepository = baseRepository;
            this._mapper = mapper;
        }
        public List<Sach> ThongTinSach()
        {
            return _baseRepository.GetAll();
        }
        public Sach LayMaSach(int maMaSach)
        {
            return _baseRepository.GetById(maMaSach); 
        }

    }
}
