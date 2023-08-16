using FappCommon.Interfaces;

namespace FappCommon.Implementations;

public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}