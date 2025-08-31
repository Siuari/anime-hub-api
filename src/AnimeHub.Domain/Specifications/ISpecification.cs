using System.Linq.Expressions;

namespace AnimeHub.Domain.Specifications
{
    public interface ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public int? Skip { get; }
        public int? Take { get; }
    }
}
