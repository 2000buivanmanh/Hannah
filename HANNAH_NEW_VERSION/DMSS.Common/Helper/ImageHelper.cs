using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace ClassLibrary1.Helper
{
    public static class ImageHelper
    {
        public static string UploadImage(HttpPostedFileBase file, string SavePatch, string SaveName)
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

        public static void GenAvatar(string FirstName, string LastName, string Patch)
        {
            List<string> _BackgroundColours = new List<string> { "#3C79B2", "#FF8F88", "#6FB9FF", "#C0CC44", "#AFB28C" };
            var avatarString = string.Format("{0}{1}", FirstName, LastName).ToUpper();
            var randomIndex = new Random().Next(0, _BackgroundColours.Count - 1);
            var bgColour = _BackgroundColours[randomIndex];
            //first, create a dummy bitmap just to get a graphics object  
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            Font font = new Font(FontFamily.GenericSerif, 45, FontStyle.Bold);
            //measure the string to see how big the image needs to be  
            SizeF textSize = drawing.MeasureString(avatarString, font);
            //free up the dummy image and old graphics object  
            img.Dispose();
            drawing.Dispose();
            //create a new image of the right size  
            img = new Bitmap(110, 110);
            drawing = Graphics.FromImage(img);
            //paint the background  
            Color bgcolor = ColorTranslator.FromHtml(bgColour);
            drawing.Clear(bgcolor);
            //create a brush for the text  
            Brush textBrush = new SolidBrush(ColorTranslator.FromHtml("#FFF"));
            //drawing.DrawString(text, font, textBrush, 0, 0);  
            drawing.DrawString(avatarString, font, textBrush, new Rectangle(-2, 20, 200, 110));
            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();
            img.Save(Patch);
        }
    }
}
