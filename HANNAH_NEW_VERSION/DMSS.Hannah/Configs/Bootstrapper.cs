using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DATA.Repository;
using Microsoft.Practices.Unity;
using SERVICE;
using SERVICE.ExampleSerivce;
using Unity.Mvc3;

namespace HANNAH_NEW_VERSION.Configs
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IExampleService, ExampleService>();
            container.RegisterType<IHangMucSachService, HangMucSachService>();
            container.RegisterType<ICaiDatService, CaiDatService>();
            container.RegisterType<ISlideService, SlideService>();
            container.RegisterType<IDacQuyenService, DacQuyenService>();
            container.RegisterType<IPhanHoiService, PhanHoiService>();
            container.RegisterType<ILoaiSachService, LoaiSachService>();
            container.RegisterType<ITheLoaiSachService, TheLoaiSachService>();
            container.RegisterType<ISachService, SachService>();
            container.RegisterType<IChiNhanhService, ChiNhanhService>();
            container.RegisterType<IDanhGiaService, DanhGiaService>();
            container.RegisterType<IChiTietDatSachService, ChiTietDatSachService>();
            container.RegisterType<IDatSachService, DatSachService>();
            container.RegisterType<IDiaChiXuatBanService, DiaChiXuatBanService>();
            container.RegisterType<IBaiVietService, BaiVietService>();
            container.RegisterType<IVideoSachService, VideoSachService>();
            container.RegisterType<IThongBaoService, ThongBaoService>();
            container.RegisterType<INhomTuoiService, NhomTuoiService>();
            container.RegisterType<IHinhAnhSachService, HinhAnhSachService>();
            container.RegisterType<INhaXuatBanService, NhaXuatBanService>();
            container.RegisterType<ITacGiaService, TacGiaService>();
            container.RegisterType<IVideoSachService, VideoSachService>();
            container.RegisterType<IAuthenticationService,AuthenticationService>();
            container.RegisterType<INguoiDungService, NguoiDungService>();
            container.RegisterType<HttpContextBase>(new InjectionFactory(_ =>
                                                    new HttpContextWrapper(HttpContext.Current)));
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>), new TransientLifetimeManager());
            var mapper = AutoMapperConfig.Configure().CreateMapper();
            container.RegisterInstance<IMapper>(mapper);
            return container;
        }
    }
}