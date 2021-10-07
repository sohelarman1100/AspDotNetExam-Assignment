using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Methods
{
    public interface IFileDownload
    {
        MemoryStream DownloadFile(string filePath);
    }
}
