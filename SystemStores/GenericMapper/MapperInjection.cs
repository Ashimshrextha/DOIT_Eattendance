﻿using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStores.GenericMapper
{
    public class MapperInjection : ConventionInjection
    {
        protected override bool Match(ConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name && !c.SourceProp.Type.IsValueType && (c.SourceProp.Type != typeof(string) && !c.SourceProp.Type.IsGenericType) && !c.TargetProp.Type.IsGenericType || c.SourceProp.Type.IsEnumerable() && c.TargetProp.Type.IsEnumerable();
        }

        protected override object SetValue(ConventionInfo c)
        {
            if (c.SourceProp.Value == null)
                return (object)null;
            return MapperUtility.Map(c.SourceProp.Value, c.TargetProp.Value, c.SourceProp.Type, c.TargetProp.Type);
        }
    }
}
