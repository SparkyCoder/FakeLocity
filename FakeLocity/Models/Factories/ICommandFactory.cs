namespace FakeLocity.Models.Factories
{
    using Commands;

    public interface ICommandFactory
    {
        T Create<T>() where T : ICommand;
        void Release(object value);
    }
}