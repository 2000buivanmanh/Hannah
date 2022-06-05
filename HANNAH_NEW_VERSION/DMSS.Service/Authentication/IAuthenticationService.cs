using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IAuthenticationService
    {
        void DangNhap(NguoiDung nguoiDung, bool createPersistentCookie);
        void DangXuat();
        NguoiDung GetAuthenticatedUser();
    }
}
