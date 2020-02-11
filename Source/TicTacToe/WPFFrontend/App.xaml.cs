using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TicTacToe.WPFFrontend.GameService;

namespace TicTacToe.WPFFrontend
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static Dictionary<Type, object> Instances = new Dictionary<Type, object>();

        public App()
        {
            StartService((Func<IGameService>)(() => { return new NoGameService(); }));
        }

        private static void StartService<T>(Func<T> ctorDefault) where T : class
        {
            string assemblyName = typeof(App).Assembly.GetName().Name;
            Type t = typeof(T);
            string envvarname = $"{nameof(T)}__Strategy";
            var typeName = Environment.GetEnvironmentVariable(envvarname);
            T service = default;
            try
            {
                service = Activator.CreateInstance(assemblyName, typeName) as T;
                if (service == null) throw new Exception($"{assemblyName} {typeName} was not {t.FullName}");
            }
            catch
            {
                service = ctorDefault();
            }
            Instances[t] = service;
        }

        internal static T LocateService<T>()
        {
            Type t = typeof(T);
            if (!Instances.ContainsKey(t)) return default;
            object obj = Instances[t];
            if (obj is T objAsT) return objAsT;
            return default;
        }
    }
}
