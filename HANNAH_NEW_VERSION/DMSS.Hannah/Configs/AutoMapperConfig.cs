using AutoMapper;
using DATA.Models;
using DMSS.ViewModals.ExampleViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HANNAH_NEW_VERSION.Configs
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return mapperConfiguration;
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateDualMapping<Example, ExampleViewModal>();
        }

        public void CreateDualMapping<TSource, TDestination>()
        {
            CreateMap<TSource, TDestination>(MemberList.Source).ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<TDestination, TSource>(MemberList.Source).ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
        }
    }
}