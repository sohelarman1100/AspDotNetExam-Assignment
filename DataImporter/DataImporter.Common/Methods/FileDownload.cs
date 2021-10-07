using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Methods
{
    public class FileDownload : IFileDownload
    {
        public MemoryStream DownloadFile(string filePath)
        {
            var memory = new MemoryStream();
            using(var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return memory;
        }
    }
}
