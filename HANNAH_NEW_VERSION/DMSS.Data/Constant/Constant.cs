using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Constant
{
    public class Constant
    {
        public class TinhTrang
        {
            public const bool IsBlocked = false;
            public const bool Activating = true;
        }
        public class PhanQuyen
        {
            public const int Admin = 0;
            public const int NguoiDung = 1;
        }
        public class TrangThaiTaiKhoan
        {
            public const int ChuaXacNhanMail = 0;
            public const int DaXacNhanMail = 1;
            public const int DaDuyet = 2;
        }

        public class TinhTrangNguoiDung
        {
            public const bool DangHoatDong = true;
            public const bool BiKhoa = false;
        }

        public class KiemTraLogin
        {
            public const int DangNhapThanhCong = 1;
            public const int DangnhapThatBai = 0;
            public const int ChuaXacNhanMail = -1;
            public const int ChuaDuyetMail = 2;
        }
        public class KiemTraDangKy
        {
            public const int DangKyThanhCong = -1; 
            public const int DaXacNhanMail = 1; 
            public const int ChuaXacNhanMail = 0; 
        }
        public class KiemTraTonTai
        {
            public const int DaTonTai = 0;
            public const int KhongTonTai = 1;

        }
        public class TrangThai
        {
            public const int ThanhCong = 1;
            public const int ThatBai = 0;
        }

        public class TinhTrangDonHang
        {
            public const int DangCho = -1;
            public const int DaHuy = 0;
            public const int DaDuyet = 1;

        }

        public class Message
        {
            public const string TaiKhoanBiKhoa = "Your account is blocked!";
            public const string SaiMatKhau = "Wrong Password!";
            public const string SaiTaiKhoan = "UserName or Email does not exit!";
            public const string QuaSoLanGuiMa = "Your account has exceeded the number of authentication attempts!";
            public const string LoiTaiLaiTrang = "Some error has occurred. please reload the page and do the operation again!";
            public const string SaiMaCapCha = "Wrong code. Please try again!";
            public const string UploadFileThanhCong = "Files Saved!";
            public const string FileNotFound = "File not found!";
            public const string FileNotSupported = "File upload not supported!";
            public const string FileTooBig = "File upload too big, max size is {0} kb!";
            public const string Nodata = "No data matching";
            public const string Success = "Success!";
            public const string Failure = "Failure!";
            public const string Updated = "Updated!";
        }
        public class AlertIcon
        {
            public const string Success = "success";
            public const string Error = "error";
            public const string Warning = "warning";
            public const string Info = "info";
            public const string Question = "question";
        }

    }
}
