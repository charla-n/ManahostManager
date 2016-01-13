using System;
using System.Linq.Expressions;

namespace ManahostManager.Utils.Mapper
{
    public class ManahostExpressMapper : IMapper
    {
        private class ExpressMapperConfig<FROM, TO> : IMapperConfig<FROM, TO>
        {
            private ExpressMapperConfig()
            { }

            public static ExpressMapperConfig<FROM, TO> Instance
            {
                get
                {
                    return INSTANCE;
                }
            }

            private static ExpressMapperConfig<FROM, TO> INSTANCE = new ExpressMapperConfig<FROM, TO>();

            public ExpressMapper.IMemberConfiguration<FROM, TO> memberConfig;

            public IMapperConfig<FROM, TO> After(Action<FROM, TO> action)
            {
                memberConfig = memberConfig.After(action);
                return this;
            }

            public IMapperConfig<FROM, TO> Before(Action<FROM, TO> action)
            {
                memberConfig = memberConfig.Before(action);
                return this;
            }

            public IMapperConfig<FROM, TO> Ignore(Expression<Func<TO, object>> expression)
            {
                memberConfig = memberConfig.Ignore(expression);
                return this;
            }

            public IMapperConfig<FROM, TO> MaxDepth(int maxDepth)
            {
                return this;
            }
        }

        public void Map<FROM, TO>(FROM from, TO to)
        {
            ExpressMapper.Mapper.Map(from, to);
        }

        public TO Map<FROM, TO>(FROM from)
        {
            return ExpressMapper.Mapper.Map<FROM, TO>(from);
        }

        public object Map(object src, Type from, Type to)
        {
            return ExpressMapper.Mapper.Map(src, from, to);
        }

        public void Reset()
        {
            ExpressMapper.Mapper.Reset();
        }

        public void Compile()
        {
            ExpressMapper.Mapper.Compile();
        }

        public IMapperConfig<FROM, TO> Register<FROM, TO>()
        {
            ExpressMapperConfig<FROM, TO> config = ExpressMapperConfig<FROM, TO>.Instance;

            config.memberConfig = ExpressMapper.Mapper.Register<FROM, TO>();
            return config;
        }
    }
}