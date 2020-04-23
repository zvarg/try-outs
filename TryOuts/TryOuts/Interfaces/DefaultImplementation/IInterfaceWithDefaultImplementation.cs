namespace TryOuts.Interfaces.DefaultImplementation
{
    interface IInterfaceWithDefaultImplementation
    {
        int Method();

        string MethodWithDefaultImplementation()
        {
            return string.Empty;
        }
    }
}
