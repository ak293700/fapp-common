using FappCommon.Interfaces;

namespace FappCommon.Mocks;

public class DateTimeServiceMock : IDateTimeService
{
    public DateTime UtcNow { get; }

    public DateTimeServiceMock(DateTime utcNow)
    {
        UtcNow = utcNow;
    }
}