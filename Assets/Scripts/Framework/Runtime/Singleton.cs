/*
 * �ļ�����Singleton.cs
 * ���ߣ�ZeroWind
 * ����ʱ�䣺2024/10/2
 * 
 * ���༭�ߣ�ZeroWind
 * ���༭ʱ�䣺2024/10/2
 * 
 * �ļ�������
 * ����ǰ��Ϊȫ�ֵ���
 */

namespace Framework.Runtime
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _value = null;

        private static readonly object _lock = new object();

        protected virtual void OnInit() { }

        public static T Instance
        {
            get
            {
                if(_value == null)
                {
                    lock (_lock)
                    {
                        if (_value == null)
                        {
                            _value = new T();
                            _value.OnInit();
                        }
                    }
                }
                return _value;
            }
        }
    }
}

