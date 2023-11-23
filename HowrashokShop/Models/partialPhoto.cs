using System.Drawing;

namespace HowrashokShop.Models
{
    partial class Photo
    {
        public Image byteArrayToImage
        {
            get
            {
                using (MemoryStream imgStream = new MemoryStream(Photo1))
                {
                    return Image.FromStream(imgStream);
                }
            }
            
        }
    }
}
