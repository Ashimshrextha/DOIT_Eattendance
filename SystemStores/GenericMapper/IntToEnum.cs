using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStores.GenericMapper
{
    public class IntToEnum : ConventionInjection
    {
        protected override bool Match(ConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name && c.SourceProp.Type == typeof(int) && c.TargetProp.Type.IsSubclassOf(typeof(Enum));
        }
    }
}
