namespace Xin.Infrastructure.Extensions;

public static class IEnumerableExtension
{
    public static IEnumerable<T> Where<T>(this IEnumerable<T> list, bool condition, Func<T, bool> exp)
    {
        if (condition)
        {
           return list.Where(exp);
        }

        return list;
    }
}