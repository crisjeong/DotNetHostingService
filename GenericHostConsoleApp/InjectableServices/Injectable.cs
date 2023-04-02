using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericHostConsoleApp.InjectableServices
{
    public interface IInjectableSerivce { }
    public interface ITransientService : IInjectableSerivce { }
    public interface IScopedService : IInjectableSerivce { }
    public interface ISingletonSerivce : IInjectableSerivce { }
}
