using System;
using System.Collections.Generic;
using System.Linq;

namespace vCard.Net.Utility;

/// <summary>
/// A collection of helper methods for working with collections.
/// </summary>
internal static class CollectionHelpers
{
    /// <summary>
    /// Computes the hash code for a collection of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="collection">The collection of items.</param>
    /// <returns>The computed hash code.</returns>
    /// <remarks>
    /// This method provides commutative, stable, and order-independent hashing for collections.
    /// </remarks>
    public static int GetHashCode<T>(IEnumerable<T> collection)
    {
        unchecked
        {
            if (collection == null)
            {
                return 0;
            }

            return collection
                .Where(e => e != null)
                .Aggregate(0, (current, element) => current + element.GetHashCode());
        }
    }

    /// <summary>
    /// Computes the hash code for a nested collection of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the nested collection.</typeparam>
    /// <param name="nestedCollection">The nested collection of items.</param>
    /// <returns>The computed hash code.</returns>
    /// <remarks>
    /// This method provides commutative, stable, and order-independent hashing for collections of collections.
    /// </remarks>
    public static int GetHashCodeForNestedCollection<T>(IEnumerable<IEnumerable<T>> nestedCollection)
    {
        unchecked
        {
            if (nestedCollection == null)
            {
                return 0;
            }

            return nestedCollection
                .SelectMany(c => c)
                .Where(e => e != null)
                .Aggregate(0, (current, element) => current + element.GetHashCode());
        }
    }

    /// <summary>
    /// Determines whether two collections are equal.
    /// </summary>
    /// <typeparam name="T">The type of items in the collections.</typeparam>
    /// <param name="left">The first collection.</param>
    /// <param name="right">The second collection.</param>
    /// <param name="orderSignificant">Indicates whether the order of elements is significant in the comparison.</param>
    /// <returns>True if the collections are equal; otherwise, false.</returns>
    public static bool Equals<T>(IEnumerable<T> left, IEnumerable<T> right, bool orderSignificant = false)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left == null && right != null)
        {
            return false;
        }

        if (left != null && right == null)
        {
            return false;
        }

        if (orderSignificant)
        {
            return left.SequenceEqual(right);
        }

        try
        {
            return left.OrderBy(l => l).SequenceEqual(right.OrderBy(r => r));
        }
        catch (Exception)
        {
            var leftSet = new HashSet<T>(left);
            var rightSet = new HashSet<T>(right);
            return leftSet.SetEquals(rightSet);
        }
    }

    /// <summary>
    /// Adds a range of elements to the collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="destination">The destination collection.</param>
    /// <param name="source">The source collection containing elements to add.</param>
    public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
    {
        foreach (var element in source)
        {
            destination.Add(element);
        }
    }
}
