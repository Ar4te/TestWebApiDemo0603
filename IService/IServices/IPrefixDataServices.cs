using Extensions.ApiContext;
using IService.Base;
using Microsoft.AspNetCore.Http;
using Models.Models;
using Models.ViewModels;

namespace IService.IServices;

public interface IPrefixDataServices : IBaseService<PrefixData>
{
    Task<MessageModel<List<PrefixDataVM>>> GetDataFromExcel(IFormFile file);
}
