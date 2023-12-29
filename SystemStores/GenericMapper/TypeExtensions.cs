using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SystemStores.GenericMapper
{
    public static class TypeExtensions
    {
        public static bool IsEnumerable(this Type type)
        {
            return type.IsGenericType && ((IEnumerable<Type>)type.GetGenericTypeDefinition().GetInterfaces()).Contains<Type>(typeof(IEnumerable));
        }
    }
}
