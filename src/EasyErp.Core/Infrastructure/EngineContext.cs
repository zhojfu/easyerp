namespace EasyErp.Core.Infrastructure
{
    using System.Configuration;
    using System.Runtime.CompilerServices;
    using EasyErp.Core.Configuration;

    public class EngineContext
    {
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }

        protected static IEngine CreateEngineInstance()
        {
            return new EasyErpEngine();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                var config = ConfigurationManager.GetSection("EasyErpConfig") as EasyErpConfig;
                Singleton<IEngine>.Instance = CreateEngineInstance();
                Singleton<IEngine>.Instance.Initialize(config);
            }
            return Singleton<IEngine>.Instance;
        }

        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }
    }
}