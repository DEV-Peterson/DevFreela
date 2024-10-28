using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.CloudServices.Interfaces
{
    internal interface IFileStoreageService
    {
        void UploadFile(byte[] content, string fileName);
    }
}
