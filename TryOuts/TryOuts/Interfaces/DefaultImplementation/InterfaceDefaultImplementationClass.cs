using System;

namespace TryOuts.Interfaces.DefaultImplementation
{
    class InterfaceDefaultImplementationClass : IInterfaceWithDefaultImplementation
    {
        public int Method() => throw new NotImplementedException();

        // This is not necessary to implement
        //public string MethodWithDefaultImplementation() => throw new NotImplementedException();
    }
}
