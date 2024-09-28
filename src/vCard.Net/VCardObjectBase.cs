namespace vCard.Net;

/// <summary>
/// Base class for vCard objects providing copy and load functionality.
/// </summary>
public class VCardObjectBase : ICopyable, ILoadable
{
    private bool _mIsLoaded;

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardObjectBase"/> class.
    /// </summary>
    public VCardObjectBase() => _mIsLoaded = true;

    /// <summary>
    /// Copies values from the target object to the current object.
    /// </summary>
    /// <param name="c">The object to copy values from.</param>
    public virtual void CopyFrom(ICopyable c) { }

    /// <summary>
    /// Creates a copy of the object.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <returns>The copy of the object.</returns>
    public virtual T Copy<T>()
    {
        var type = GetType();
        var obj = Activator.CreateInstance(type) as ICopyable;

        // Duplicate our values
        if (obj is T t)
        {
            obj.CopyFrom(this);
            return t;
        }
        return default;
    }

    /// <summary>
    /// Gets a value indicating whether the object has been loaded.
    /// </summary>
    public virtual bool IsLoaded => _mIsLoaded;

    /// <summary>
    /// Event that fires when the object has been loaded.
    /// </summary>
    public event EventHandler Loaded;

    /// <summary>
    /// Signals that the object has been loaded.
    /// </summary>
    public virtual void OnLoaded()
    {
        _mIsLoaded = true;
        Loaded?.Invoke(this, EventArgs.Empty);
    }
}