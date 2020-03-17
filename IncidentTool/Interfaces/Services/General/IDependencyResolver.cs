using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Interfaces.Services.General
{
    public interface IDependencyResolver
    {
        object Resolve(Type typeName);
        T Resolve<T>();
    }
}
