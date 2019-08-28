using System.Reflection;

namespace Blaxpro.Automap.Models
{
    public class PropertyMap
    {
        public PropertyInfo SourceProperty { get; set; }
        public PropertyInfo TargetProperty { get; set; }
    }
}