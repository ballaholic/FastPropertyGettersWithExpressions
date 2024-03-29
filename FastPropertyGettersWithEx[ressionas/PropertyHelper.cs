﻿namespace FastPropertyGettersWithExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Linq;
    using System.Collections.Concurrent;

    public class PropertyHelper
    {
        private static readonly Type TypeOfObject = typeof(object);

        private static ConcurrentDictionary<Type, PropertyHelper[]> cache = new ConcurrentDictionary<Type, PropertyHelper[]>();
        public string Name { get; set; }

        // obj => obj.Property;
        public Func<object, object> Getter { get; set; }

        public static PropertyHelper[] GetProperties(Type type)
            => cache.GetOrAdd(type, _ =>
            {
                return type
                .GetProperties()
                .Select(pr =>
                {
                    // Object obj
                    var parameter = Expression.Parameter(TypeOfObject, "obj");

                    // (T)obj
                    var parameterConvert = Expression.Convert(parameter, type);

                    // ((T)obj).Property
                    var body = Expression.MakeMemberAccess(parameterConvert, pr);

                    // (object)((T)obj).Property
                    var convertedBody = Expression.Convert(body, TypeOfObject);

                    // Object obj => ((T)obj).Property
                    var lambda = Expression.Lambda<Func<object, object>>(convertedBody, parameter);

                    var propertyGetterFunc = lambda.Compile();

                    return new PropertyHelper
                    {
                        Name = pr.Name,
                        Getter = propertyGetterFunc
                    };
                })
                .ToArray();
            });
    }
}
