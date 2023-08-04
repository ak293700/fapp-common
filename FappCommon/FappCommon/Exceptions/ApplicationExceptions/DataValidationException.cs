using System.Diagnostics.CodeAnalysis;
using FappCommon.Exceptions.Base;

namespace FappCommon.Exceptions.ApplicationExceptions;

public class DataValidationException : CustomException
{
    public DataValidationException(string message) : base(message)
    {
    }

    public static DataValidationException Instance => new("Data validation failed");

    public static DataValidationException WithPropertyName([ConstantExpected] string propertyName)
    {
        return new DataValidationException($"Field {propertyName} is invalid");
    }

    public static DataValidationException FromRelatedDataWithPropertyName([ConstantExpected] string property1Name,
        [ConstantExpected] string property2Name)
    {
        return new DataValidationException($"Data validation failed from related {property1Name} and {property2Name}");
    }

    public static DataValidationException FromRelatedDataWithPropertyName([ConstantExpected] string property1Name,
        [ConstantExpected] string property2Name, [ConstantExpected] string property3Name)
    {
        return new DataValidationException(
            $"Data validation failed from related {property1Name}, {property2Name} and {property3Name}");
    }
}