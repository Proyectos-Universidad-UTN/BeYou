namespace BeYou.Common.Extensions;

/// <summary>
/// Extension methods for collections. 
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Checks if the collection has any items.
    /// </summary>
    /// <param name="source">Elements source</param>
    /// <typeparam name="T">Any</typeparam>
    /// <returns>True if has items, false if not</returns>
    public static bool HasItems<T>(this IEnumerable<T> source) => source?.Any() ?? false;

    /// <summary>
    /// Checks if the collection is empty.
    /// </summary>
    /// <param name="destination">Destination collection of object</param>
    /// <param name="source">Elements source</param>
    /// <typeparam name="T">Any</typeparam>
    public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            destination.Add(item);
        }
    }

    /// <summary>
    /// Sorts a list of objects based on a specified key.
    /// </summary>
    /// <param name="list">List of elements</param>
    /// <param name="selector">Function selector</param>
    /// <param name="comparer">Comparer Key</param>
    /// <typeparam name="T">Any</typeparam>
    /// <typeparam name="TKey">Key Any</typeparam>
    public static void SortBy<T, TKey>(this List<T> list, Func<T, TKey> selector, IComparer<TKey>? comparer = null)
    {
        comparer ??= Comparer<TKey>.Default;
        list.Sort((x, y) => comparer.Compare(selector(x), selector(y)));
    }

    /// <summary>
    /// Sorts a list of objects in descending order based on a specified key.
    /// </summary>
    /// <param name="list">List of elements</param>
    /// <param name="selector">Function selector</param>
    /// <param name="comparer">Comparer Key</param>
    /// <typeparam name="T">Any</typeparam>
    /// <typeparam name="TKey">Key Any</typeparam>
    public static void SortByDescending<T, TKey>(this List<T> list, Func<T, TKey> selector, IComparer<TKey>? comparer = null)
    {
        comparer ??= Comparer<TKey>.Default;
        list.Sort((x, y) => comparer.Compare(selector(y), selector(x)));
    }
}
