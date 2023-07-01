using IService.IServices;
using Microsoft.Extensions.Logging;
using Models.Models;
using Repository.Base;
using Service.Base;

namespace Service.Services;

public class SpotCheckRecordService : BaseService<SpotCheckRecord>, ISpotCheckRecordService
{
    private readonly ILogger<SpotCheckService> _logger;
    private readonly IBaseRepository<SpotCheckRecord> _dal;
    public SpotCheckRecordService(IBaseRepository<SpotCheckRecord> dal, ILogger<SpotCheckService> logger)
    {
        _logger = logger;
        _baseDal = dal;
        _dal = dal;
    }
}
