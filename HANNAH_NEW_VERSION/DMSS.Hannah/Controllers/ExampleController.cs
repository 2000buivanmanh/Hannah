using SERVICE.ExampleSerivce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HANNAH_NEW_VERSION.Controllers
{
    public class ExampleController : Controller
    {
        private readonly IExampleService _exampleService;

        public ExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        // GET: Example
        public ActionResult Index()
        {
            var Data = _exampleService.GetAll();
            foreach(var item in Data)
            {
                item.ThuocTinhThem = item.ThuocTinh + "Đã custom nha";
            }
            return View(Data);
        }
    }
}