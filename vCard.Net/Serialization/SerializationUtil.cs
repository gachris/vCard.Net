using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace vCard.Net.Serialization;

/// <summary>
/// Utility class for invoking methods decorated with serialization attributes.
/// </summary>
public class SerializationUtil
{
    /// <summary>
    /// Invokes methods decorated with the <see cref="OnDeserializingAttribute"/> attribute.
    /// </summary>
    /// <param name="obj">The object on which to invoke methods.</param>
    public static void OnDeserializing(object obj)
    {
        foreach (var mi in GetDeserializingMethods(obj.GetType()))
        {
            mi.Invoke(obj, [new StreamingContext()]);
        }
    }

    /// <summary>
    /// Invokes methods decorated with the <see cref="OnDeserializedAttribute"/> attribute.
    /// </summary>
    /// <param name="obj">The object on which to invoke methods.</param>
    public static void OnDeserialized(object obj)
    {
        foreach (var mi in GetDeserializedMethods(obj.GetType()))
        {
            mi.Invoke(obj, [new StreamingContext()]);
        }
    }

    private const BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

    private static readonly ConcurrentDictionary<Type, List<MethodInfo>> _onDeserializingMethods = new ConcurrentDictionary<Type, List<MethodInfo>>();
    private static List<MethodInfo> GetDeserializingMethods(Type targetType)
    {
        if (targetType == null)
        {
            return new List<MethodInfo>();
        }

        if (_onDeserializingMethods.ContainsKey(targetType))
        {
            return _onDeserializingMethods[targetType];
        }

        return _onDeserializingMethods.GetOrAdd(targetType, tt => tt
        .GetMethods(_bindingFlags)
        .Where(targetTypeMethodInfo => targetTypeMethodInfo
            .GetCustomAttributes(typeof(OnDeserializingAttribute), false).Any())
        .ToList());
    }

    private static readonly ConcurrentDictionary<Type, List<MethodInfo>> _onDeserializedMethods = new ConcurrentDictionary<Type, List<MethodInfo>>();
    private static List<MethodInfo> GetDeserializedMethods(Type targetType)
    {
        if (targetType == null)
        {
            return new List<MethodInfo>();
        }
        if (_onDeserializedMethods.TryGetValue(targetType, out List<MethodInfo> methodInfos))
        {
            return methodInfos;
        }

        methodInfos = targetType.GetMethods(_bindingFlags)
            .Select(targetTypeMethodInfo => new
            {
                targetTypeMethodInfo,
                attrs = targetTypeMethodInfo.GetCustomAttributes(typeof(OnDeserializedAttribute), false).ToList()
            })
            .Where(t => t.attrs.Count > 0)
            .Select(t => t.targetTypeMethodInfo)
            .ToList();

        _onDeserializedMethods.AddOrUpdate(targetType, methodInfos, (type, list) => methodInfos);
        return methodInfos;
    }
}