using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataStructures.Observer_Pattern
{
    public abstract class Observable<T> where T : Observable<T>
    {
        private readonly List<Observer<T>>
            _observers = new List<Observer<T>>(); //All subscripers to this observable

        private readonly Dictionary<string, Property>
            _properties = new Dictionary<string, Property>(); //All properties of this observable

        /// <summary>
        /// Get the property value hashed at the key value
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <returns>Property value or null if the key does not exist</returns>
        protected dynamic Get([CallerMemberName] string key = null)
        {
            Property property;
            return key != null && _properties.TryGetValue(key, out property) ? property.Value : null;
        }

        /// <summary>
        /// Creates or updates the property value at the key value
        /// </summary>
        /// <param name="value">Property value</param>
        /// <param name="key">Hash key</param>
        protected void Set(object value, [CallerMemberName] string key = null)
        {
            if (key == null) return;
            if (_properties.ContainsKey(key))
            {
                _properties[key].Value = value;
            }
            else
            {
                var property = new Property(value);
                property.PropertyChanged += Update;
                _properties.Add(key, property);
            }
        }

        /// <summary>
        /// Adds observer to observer list
        /// </summary>
        /// <param name="observer">Observer</param>
        protected void Subscribe(Observer<T> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        /// <summary>
        /// Removes observer from observer list
        /// </summary>
        /// <param name="observer">Observer</param>
        protected void Unsubscribe(Observer<T> observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        /// <summary>
        /// Updates all of the observers subscribed to this observable when a property value is changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void Update(object sender, EventArgs e)
        {
            var update = new List<Observer<T>>();
            var reset = new List<Observer<T>>();

            foreach (var observer in _observers)
            {
                if(observer.UpdateCondition((T) this))
                    update.Add(observer);
                else
                    reset.Add(observer);
            }

            update.ForEach(observer => observer.Update((T) this));
            reset.ForEach(observer => observer.Skip = false);
        }
    }
}