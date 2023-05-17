using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Base.Users.Security;
using SavaDev.Files.Data.Contract;
using SavaDev.Files.Data.Contract.Models;
using SavaDev.Files.Front.Contract;
using SavaDev.Files.Front.Contract.Models;
using SavaDev.Files.Service.Contract;
using SavaDev.Files.Service.Contract.Models;

namespace SavaDev.Files.Front.Services
{
    public class FileViewService : IFileViewService
    {
        private readonly IFilesUploader _fileUploader;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public FileViewService(IFilesUploader fileProcessingService, 
            IFileService fileService,
            IUserProvider userProvider,
            IMapper mapper)
        {
            _fileUploader = fileProcessingService;
            _fileService = fileService;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task Upload(Stream stream)
        {
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var content = ms.ToArray();
            var uploadedFileModel = await _fileUploader.SendInfo(new FilesDataModel(content, _userProvider.UserId));
        }

        public async Task DownloadAndSaveFile(string fileUri)
        {
            using var client = new HttpClient();
            var content = await client.GetByteArrayAsync(fileUri);

            var uploadedFileModel = await _fileUploader.SendInfo(new FilesDataModel(content, _userProvider.UserId));
        }

        public async Task<RegistryPageViewModel<FileViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<FileModel, FileFilterModel>(_fileService, _mapper);
            var vm = await manager.GetRegistryPage<FileViewModel>(query);
            return vm;
        }
    }
}
