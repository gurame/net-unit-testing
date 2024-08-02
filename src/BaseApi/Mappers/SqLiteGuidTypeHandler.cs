using System.Data;
using static Dapper.SqlMapper;

namespace BaseApi.Mappers;
public class SqLiteGuidTypeHandler : ITypeHandler
{
    public object? Parse(Type destinationType, object value)
    {
        return new Guid((string)value);
    }

    public void SetValue(IDbDataParameter parameter, object value)
    {
        parameter.DbType = DbType.String;
		parameter.Value = value.ToString();
    }
}
