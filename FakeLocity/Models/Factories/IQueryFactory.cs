namespace FakeLocity.Models.Factories
{
    using Queries;

    public interface IQueryFactory
    {
        T Create<T>() where T : IQuery;
        void Release(object value);
    }
}