using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOSOT_Praktika
{
    class ConvertImageToByteClass
    {
        public static byte[] ImageToByte(string strPath)

        {
            byte[] image;

            if (string.IsNullOrEmpty(strPath))
            {
                var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string filePath = Path.Combine(projectPath, "Resources/nophoto.jpg");
                image = File.ReadAllBytes(filePath);
            }
            else
            {
                image = File.ReadAllBytes(strPath);
            }
            return image;
        }
    }
}
