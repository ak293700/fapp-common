using FappCommon.Interfaces;

namespace FappCommon.Mocks;

public class DynamicDateTimeMockService : IDateTimeService
{
    public DateTime UtcNow { get; set; }

    public DynamicDateTimeMockService()
    {
        UtcNow = DateTime.UtcNow;
    }

    public DynamicDateTimeMockService(DateTime dateTime)
    {
        UtcNow = dateTime;
    }
}