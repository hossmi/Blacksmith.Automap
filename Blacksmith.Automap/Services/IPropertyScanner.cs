using Blacksmith.Automap.Models;
using System.Collections.Generic;

namespace Blacksmith.Automap.Services
{
    public interface IPropertyScanner
    {
        bool canScan(object item);
        IDictionary<string, IPropertyAccessor> getReadProperties(object source);
        IDictionary<string, IPropertyAccessor> getWriteProperties(object source);
    }
}