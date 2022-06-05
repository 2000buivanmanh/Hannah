using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClassLibrary1.Helper
{
    public static class FileHelper
    {
        public static string UploadFile(HttpPostedFileBase file, string SavePatch, string SaveName)
        {
            try
            {
                if (file != null)
                {
                    if (!System.IO.Directory.Exists(SavePatch))
                    {
                        System.IO.Directory.CreateDirectory(SavePatch); //Create directory if it doesn't exist
                    }
                    string urlAnh = Path.Combine(SavePatch, SaveName);
                    file.SaveAs(urlAnh);
                    return string.Empty;
                }
                else return "File Not Found!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

    }
}
