using DATA.Models;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public class VideoSachService : IVideoSachService
    {
        private readonly IBaseRepository<VideoSach> _baseRepository;

        public VideoSachService(IBaseRepository<VideoSach> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<VideoSach> DanhSachVideoSach()
        {
            return _baseRepository.GetAll();
        }
    }
}
