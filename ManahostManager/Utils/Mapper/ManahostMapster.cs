using Mapster;
using System;
using System.Linq.Expressions;

namespace ManahostManager.Utils.Mapper
{
    public class ManahostMapster : IMapper
    {
        private class MapsterConfig<FROM, TO> : IMapperConfig<FROM, TO>
        {
            private MapsterConfig()
            { }

            public static MapsterConfig<FROM, TO> Instance
            {
                get
                {
                    return INSTANCE;
                }
            }

            private static MapsterConfig<FROM, TO> INSTANCE = new MapsterConfig<FROM, TO>();

            public TypeAdapterConfig<FROM, TO> memberConfig;

            public IMapperConfig<FROM, TO> After(Action<FROM, TO> action)
            {
                //memberConfig = memberConfig.After(action);
                return this;
            }

            public IMapperConfig<FROM, TO> Before(Action<FROM, TO> action)
            {
                //memberConfig = memberConfig.Before(action);
                return this;
            }

            public IMapperConfig<FROM, TO> Ignore(Expression<Func<TO, object>> expression)
            {
                memberConfig = memberConfig.Ignore(expression);
                return this;
            }

            public IMapperConfig<FROM, TO> MaxDepth(int maxDepth)
            {
                memberConfig.MaxDepth(maxDepth);
                return this;
            }
        }

        public void Map<FROM, TO>(FROM from, TO to)
        {
            TypeAdapter.Adapt(from, to);
        }

        public TO Map<FROM, TO>(FROM from)
        {
            return TypeAdapter.Adapt<TO>(from);
        }

        public object Map(object src, Type from, Type to)
        {
            return TypeAdapter.Adapt(src, from, to);
        }

        public void Reset()
        {
        }

        public void Compile()
        {
        }

        public IMapperConfig<FROM, TO> Register<FROM, TO>()
        {
            MapsterConfig<FROM, TO> config = MapsterConfig<FROM, TO>.Instance;

            config.memberConfig = TypeAdapterConfig<FROM, TO>.NewConfig();
            return config;
        }
    }
}