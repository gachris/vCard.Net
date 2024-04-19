using System.Reflection;
using Xunit.Sdk;

namespace vCard.Net.Tests;

public sealed class InlineDataExAttribute : DataAttribute
{
    private readonly object?[] _args;

    public InlineDataExAttribute(params object?[] args)
    {
        _args = args;
    }

    public override IEnumerable<object?[]> GetData(MethodInfo testMethod)
    {
        var result = new object?[_args.Length];
        for (var index = 0; index < _args.Length; index++)
        {
            result[index] = ReadManifestData(_args[index]);
        }
        return new[] { result };
    }

    public static object? ReadManifestData(object? value)
    {
        if (value is string textValue)
        {
            var assembly = typeof(InlineDataExAttribute).GetTypeInfo().Assembly;
            var resourceName = assembly.GetName().Name + "." + textValue.Replace("/", ".");
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return textValue;
            }
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        else return value;
    }
}