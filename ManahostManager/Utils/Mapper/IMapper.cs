using System;

namespace ManahostManager.Utils.Mapper
{
    public interface IMapper
    {
        void Map<FROM, TO>(FROM from, TO to);

        TO Map<FROM, TO>(FROM from);

        object Map(object src, Type from, Type to);

        void Reset();

        void Compile();

        IMapperConfig<FROM, TO> Register<FROM, TO>();
    }

    public interface IMapperConfig<FROM, TO>
    {
        IMapperConfig<FROM, TO> After(Action<FROM, TO> action);

        IMapperConfig<FROM, TO> Before(Action<FROM, TO> action);

        IMapperConfig<FROM, TO> MaxDepth(int maxDepth);

        IMapperConfig<FROM, TO> Ignore(System.Linq.Expressions.Expression<Func<TO, object>> expression);
    }
}