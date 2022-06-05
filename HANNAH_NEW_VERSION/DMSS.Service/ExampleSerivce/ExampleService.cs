using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA.Repository;
using DMSS.ViewModals.ExampleViewModal;
using SERVICE.BaseService;
using AutoMapper;

namespace SERVICE.ExampleSerivce
{
    public class ExampleService : BaseService<Example, ExampleViewModal> , IExampleService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Example> _baseRepository;

        public ExampleService(IBaseRepository<Example> baseRepository,IMapper mapper) : base(baseRepository, mapper)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }
    }
}
