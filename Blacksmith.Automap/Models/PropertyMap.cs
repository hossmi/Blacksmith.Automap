using System.Reflection;

namespace Blacksmith.Automap.Models
{
    public class PropertyMap
    {
        public IPropertyAccessor SourceProperty { get; set; }
        public IPropertyAccessor TargetProperty { get; set; }
    }
}