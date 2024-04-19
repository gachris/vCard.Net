using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace vCard.Net.Collections.Proxies;

/// <summary>
/// Represents a proxy for a keyed list.
/// </summary>
/// <typeparam name="TGroup">The type of the key used for grouping.</typeparam>
/// <typeparam name="TOriginal">The type of the original objects in the collection.</typeparam>
/// <typeparam name="TNew">The type of the new objects in the collection.</typeparam>
public class GroupedCollectionProxy<TGroup, TOriginal, TNew> :
    IGroupedCollection<TGroup, TNew>
    where TOriginal : class, IGroupedObject<TGroup>
    where TNew : class, TOriginal
{
    private readonly Func<TNew, bool> _predicate;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupedCollectionProxy{TGroup, TOriginal, TNew}"/> class.
    /// </summary>
    /// <param name="realObject">The original grouped collection to proxy.</param>
    /// <param name="predicate">An optional predicate function to filter items in the collection.</param>
    public GroupedCollectionProxy(IGroupedCollection<TGroup, TOriginal> realObject, Func<TNew, bool> predicate = null)
    {
        _predicate = predicate ?? (o => true);
        SetProxiedObject(realObject);
    }

    /// <inheritdoc/>
    public virtual event EventHandler<ObjectEventArgs<TNew, int>> ItemAdded;

    /// <inheritdoc/>
    public virtual event EventHandler<ObjectEventArgs<TNew, int>> ItemRemoved;
   
    /// <summary>
    /// Invokes the ItemAdded event.
    /// </summary>
    /// <param name="item">The item that was added.</param>
    /// <param name="index">The index at which the item was added.</param>
    protected void OnItemAdded(TNew item, int index) => ItemAdded?.Invoke(this, new ObjectEventArgs<TNew, int>(item, index));

    /// <summary>
    /// Invokes the ItemRemoved event.
    /// </summary>
    /// <param name="item">The item that was removed.</param>
    /// <param name="index">The index from which the item was removed.</param>
    protected void OnItemRemoved(TNew item, int index) => ItemRemoved?.Invoke(this, new ObjectEventArgs<TNew, int>(item, index));

    /// <inheritdoc/>
    public virtual bool Remove(TGroup group) => RealObject.Remove(group);

    /// <inheritdoc/>
    public virtual void Clear(TGroup group) => RealObject.Clear(group);

    /// <inheritdoc/>
    public virtual bool ContainsKey(TGroup group) => RealObject.ContainsKey(group);

    /// <inheritdoc/>
    public virtual int CountOf(TGroup group) => RealObject.OfType<TGroup>().Count();

    /// <inheritdoc/>
    public virtual IEnumerable<TNew> AllOf(TGroup group) => RealObject
        .AllOf(group)
        .OfType<TNew>()
        .Where(_predicate);

    /// <inheritdoc/>
    public virtual void Add(TNew item) => RealObject.Add(item);

    /// <inheritdoc/>
    public virtual void Clear()
    {
        // Only clear items of this type
        // that match the predicate.

        var items = RealObject
            .OfType<TNew>()
            .ToArray();

        foreach (var item in items)
        {
            RealObject.Remove(item);
        }
    }

    /// <inheritdoc/>
    public virtual bool Contains(TNew item) => RealObject.Contains(item);

    /// <inheritdoc/>
    public virtual void CopyTo(TNew[] array, int arrayIndex)
    {
        var i = 0;
        foreach (var item in this)
        {
            array[arrayIndex + i++] = item;
        }
    }

    /// <inheritdoc/>
    public virtual int Count => RealObject
        .OfType<TNew>()
        .Count();

    /// <inheritdoc/>
    public virtual bool IsReadOnly => false;

    /// <inheritdoc/>
    public virtual bool Remove(TNew item) => RealObject.Remove(item);

    /// <inheritdoc/>
    public virtual IEnumerator<TNew> GetEnumerator() => RealObject
        .OfType<TNew>()
        .GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => RealObject
        .OfType<TNew>()
        .GetEnumerator();

    /// <summary>
    /// Gets or sets the real grouped collection being proxied.
    /// </summary>
    public IGroupedCollection<TGroup, TOriginal> RealObject { get; private set; }

    /// <summary>
    /// Sets the real grouped collection being proxied.
    /// </summary>
    /// <param name="realObject">The real grouped collection to set.</param>
    public virtual void SetProxiedObject(IGroupedCollection<TGroup, TOriginal> realObject) => RealObject = realObject;
}