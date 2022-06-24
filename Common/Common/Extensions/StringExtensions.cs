namespace Kvpbldsck.NastolochkiAPI.Common.Contract.Extensions;

public static class StringExtensions
{
    public static string? Append(this string? str, object toAppend)
    {
        if (str is null)
        {
            return str;
        }

        return str + toAppend;
    }
}
