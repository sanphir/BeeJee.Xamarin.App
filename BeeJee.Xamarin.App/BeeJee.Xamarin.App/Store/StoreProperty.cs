using System;
using System.Collections.Generic;

namespace BeeJee.Xamarin.App.Store
{
    public class StoreProperty<TValue>
    {
        private TValue _value;
        private readonly Dictionary<Type, Action<TValue>> _selectors = new Dictionary<Type, Action<TValue>>();

        public StoreProperty(TValue defaultValue)
        {
            _value = defaultValue;
        }

        public void AddSelector(Type selectorType, Action<TValue> action)
        {
            _selectors[selectorType] = action;
        }

        public void RemoveSelector(Type selectorType)
        {
            _selectors.Remove(selectorType);
        }

        public void SetValue(TValue value)
        {
            _value = value;
            foreach (var selector in _selectors)
            {
                selector.Value(value);
            }
        }

        public TValue GetValue() => _value;
    }
}
