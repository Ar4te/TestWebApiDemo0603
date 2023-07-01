using Extensions.ApiContext;
using IService.Base;
using Models.Models;
using Models.ViewModels;

namespace IService.IServices;

public interface ISpotCheckService : IBaseService<SpotCheck>
{
    Task GetSCPlan();
    Task<MessageModel<string>> MarkDownSCPlan(SpotCheckVM scvm);
}
