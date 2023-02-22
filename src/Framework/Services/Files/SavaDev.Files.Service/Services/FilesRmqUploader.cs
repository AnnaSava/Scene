using EasyNetQ;
using SavaDev.Files.Service.Contract;
using SavaDev.Files.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Files.Service.Services
{
    public class FilesRmqUploader : IFilesUploader
    {
        private readonly string _host = "host=localhost";

        public async Task<FilesUploadResult> SendInfo(FilesDataModel info)
        {
            using (var bus = RabbitHutch.CreateBus(_host))
            {
                var result = await bus.Rpc.RequestAsync<FilesDataModel, FilesUploadResult>(info);
                return result;
            }
        }
    }
}
