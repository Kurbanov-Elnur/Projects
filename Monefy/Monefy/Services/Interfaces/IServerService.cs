using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

interface IServerService
{
    public string DownloadFile(string filePath);
    public void UploadFile(string filePath, string text);
}
