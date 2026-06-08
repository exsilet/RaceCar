using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

//using YG;

namespace DefaultNamespace
{
    public class DropdownLanguage : MonoBehaviour
    {
        [SerializeField] private Dropdown _dropdown;
        [SerializeField] private Text _labelText;
        [SerializeField] private Text _itemText;
        
        [Header("Переводы для каждого языка (3 элемента: пункты дропдауна)")]
        [FormerlySerializedAs("ru")] [SerializeField] private string[] _Ru = new string[3];
        [FormerlySerializedAs("en")] [SerializeField] private string[] _En = new string[3];
        [FormerlySerializedAs("tr")] [SerializeField] private string[] _Tr = new string[3];
        
        private int _fontNumber = 0;
        private string _languageStart;
        private int _labelBaseFontSize, _itemBaseFontSize;
        
        private void OnEnable()
        {
            switch (YG2.lang)
            {
                case "en":
                    _dropdown.value = 0;
                    ApplyLanguage(_En, 0);
                    break;
                case "ru":
                    _dropdown.value = 1;
                    ApplyLanguage(_Ru, 1);
                    break;
                case "tr":
                    _dropdown.value = 2;
                    ApplyLanguage(_Tr, 2);
                    break;
                default:
                    _dropdown.value = 1;
                    ApplyLanguage(_Ru, 1);
                    break;
            }
        }

        public void InputLanguage(int value)
        {
            string[] lang;
            string langCode;
 
            switch (value)
            {
                case 0:  lang = _En; langCode = "en"; break;
                case 2:  lang = _Tr; langCode = "tr"; break;
                default: lang = _Ru; langCode = "ru"; break;
            }
            
            YG2.SwitchLanguage(langCode);
            ApplyLanguage(lang, value);
        }
 
        private void ApplyLanguage(string[] language, int index)
        {
            for (int i = 0; i < language.Length && i < _dropdown.options.Count; i++)
                _dropdown.options[i].text = language[i];
 
            _labelText.text = _dropdown.options[index].text;
            _dropdown.RefreshShownValue();
        }
    }
}