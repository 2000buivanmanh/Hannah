using DATA.Models;
using DMSS.ViewModals.ExampleViewModal;
using SERVICE.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.ExampleSerivce
{
    public interface IExampleService : IBaseService<Example, ExampleViewModal>
    {

    }
}
