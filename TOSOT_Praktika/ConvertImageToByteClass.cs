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
        public static byte[] ImageToByte(string Path)

        {

            byte[] image;

            image = File.ReadAllBytes(Path);

            return image;
            

        }
    }
}
