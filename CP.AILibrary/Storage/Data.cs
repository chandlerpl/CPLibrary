using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.AILibrary.Storage
{
    [Serializable]
    public abstract class Data
    {
        private string _name;
        private Guid _id;
        private bool isConstant = false;

        public event Action<string> onNameChanged;
        public event Action<string, object, object> onValueChanged;
        
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    onNameChanged?.Invoke(value);
                }
            }
        }

        public Guid ID
        {
            get
            {
                if (_id == Guid.Empty)
                {
                    _id = Guid.NewGuid();
                }
                return _id;
            }
        }

        public bool IsConstant
        {
            get => isConstant;
            private set => isConstant = value;
        }

        public Data() { }

        protected void OnValueChanged(string Name, object val, object oldVal)
        {
            onValueChanged?.Invoke(Name, val, oldVal);
        }

        abstract protected object objectValue { get; set; }
        public object value
        {
            get { return objectValue; }
            set { objectValue = value; }
        }

        abstract public Type dataType { get; }
        
        public void makeConstant()
        {
            IsConstant = true;
        }

        internal void makeConstant(bool c)
        {
            IsConstant = c;
        } 
        
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return ID == ((Data)obj).ID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [Serializable]

    public class Data<T> : Data
    {
        protected T _value;

        public override Type dataType => typeof(T);

        public Data() { }

        protected override object objectValue
        {
            get { return value; }
            set { this.value = (T)value; }
        }

        new public T value
        {
            get => _value;
            set
            {
                if(!IsConstant)
                {
                    T oldValue = _value;
                    _value = value;
                    OnValueChanged(Name, value, oldValue);
                }
            }
        }
    }
}
