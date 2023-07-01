using Extensions.ApiContext;
using IService.Base;
using Microsoft.AspNetCore.Http;
using Models.Models;

namespace IService.IServices;

public interface IPrefixDataServices : IBaseService<PrefixData>
{
    Task<MessageModel<List<PrefixData>>> GetDataFromExcel(IFormFile file);
}
