using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Services
{
    public interface IFileService
    {
        Task WriteTextAsync(string name, string content);
    }
}
