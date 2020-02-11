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

        private static void EnsureServiceListings()
        {
            if (Instances.Count > 0) return;
            StartService((Func<IGameService>)(() => { return new CoreGameService(); }));
        }

        private static void StartService<T>(Func<T> ctorDefault) where T : class
        {
            Type interfaceType = typeof(T);
            string assemblyName = interfaceType.Assembly.GetName().Name;
            string typeKey = $"{interfaceType.Name}__Strategy";
            var typeValue = Environment.GetEnvironmentVariable(typeKey);
            
            T service = default;
            try
            {
                Type instanceType = Type.GetType(typeValue);
                var ctor = instanceType.GetConstructor(Type.EmptyTypes);
                object instance = ctor.Invoke(null);
                service = instance as T;
                if (service == null) throw new Exception($"{assemblyName} {typeValue} was not {interfaceType.FullName}");
            }
            catch
            {
                service = ctorDefault();
            }
            Instances[interfaceType] = service;
        }

        internal static T LocateService<T>()
        {
            EnsureServiceListings();
            Type t = typeof(T);
            if (!Instances.ContainsKey(t)) return default;
            object obj = Instances[t];
            if (obj is T objAsT) return objAsT;
            return default;
        }
    }
}
