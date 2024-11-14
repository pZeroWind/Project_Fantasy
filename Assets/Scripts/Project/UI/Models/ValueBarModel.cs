
namespace Project.UI.Models
{
    public class ValueBarModel
    {
        private string _name;

        private float _value;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name 
        { 
            get => _name; 
            set => _name = value; 
        }

        /// <summary>
        /// 当前值
        /// </summary>
        public float Value
        { 
            get => _value;
            set
            {
                if (value >= 0 && value <= 1)
                {
                    _value = value;
                }
            }
        }

        public bool TryCheckValue(float curValue, float maxValue, out float ratio)
        {
            ratio = 0;
            if (curValue >= 0 && maxValue >= 0 && curValue <= maxValue)
            {
                ratio = curValue / maxValue;
                return true;
            }
            return false;
        }
    }
}
