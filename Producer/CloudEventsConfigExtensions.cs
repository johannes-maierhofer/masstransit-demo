using System.Reflection;
using CloudEventify;

namespace Producer;

public static class CloudEventsConfigExtensions
{
    public static IMap MapAllTypes(this IMap typeMapper, params Assembly[] assemblies)
    {
        var mapMethod = typeof(IMap)
            .GetMethods()
            .FirstOrDefault(m => m.Name == "Map" && m.IsGenericMethod);

        if (mapMethod == null)
        {
            throw new InvalidOperationException("Map method not found on CloudEventify.IMap.");
        }

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            !typeof(Attribute).IsAssignableFrom(t))
                .ToList();

            foreach (var type in types)
            {
                try
                {
                    var genericMapMethod = mapMethod.MakeGenericMethod(type);
                    genericMapMethod.Invoke(typeMapper, new object[] { type.FullName! });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to map type {type.FullName}: {ex.Message}");
                }
            }
        }

        return typeMapper;
    }
}
