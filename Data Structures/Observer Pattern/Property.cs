using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataStructures.Annotations;

namespace DataStructures.Observer_Pattern
{
    internal class Property : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; //Property changed event

        private object _value; //Property value
        private readonly Type _type; //Property type

        public object Value
        {
            get { return _value; }
            set
            {
                if (value.GetType() != _type || value.Equals(_value)) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A reworking of the type 'object' where an event is activated when the value is changed
        /// </summary>
        /// <param name="value">The type and value of the property</param>
        internal Property(object value)
        {
            _value = value;
            _type = value.GetType();
        }

        /// <summary>
        /// Inovkes the property changed event if the stored value is changed
        /// </summary>
        /// <param name="propertyName">Name assigned to the value</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}