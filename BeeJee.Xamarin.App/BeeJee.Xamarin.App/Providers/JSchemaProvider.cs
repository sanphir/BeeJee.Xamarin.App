using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;

namespace BeeJee.Xamarin.App.Providers
{
    /// <summary>
    /// Синлетон дле кеширования json схем
    /// </summary>
    public sealed class JSchemaProvider
    {
        private static JSchemaProvider _instance = null;
        private static readonly object _lock = new object();

        private readonly JSchemaGenerator _generator = new JSchemaGenerator();
        private readonly Dictionary<Type, JSchema> _shemes = new Dictionary<Type, JSchema>();

        private JSchemaProvider()
        {
        }

        public static JSchemaProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new JSchemaProvider();
                        }
                    }
                }
                return _instance;
            }
        }

        public JSchema GetJSchema(Type type)
        {
            if (_shemes.TryGetValue(type, out JSchema result))
            {
                return result;
            }

            lock (_lock)
            {
                _shemes.Add(type, _generator.Generate(type));
            }

            return _shemes[type];
        }
    }
}
