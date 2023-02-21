using Sava.Media.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Media.Front.Contract
{
    public interface IGalleryViewService
    {
        Task<RegistryPageViewModel<GalleryViewModel>> GetRegistryPage(RegistryQuery query);
    }
}
