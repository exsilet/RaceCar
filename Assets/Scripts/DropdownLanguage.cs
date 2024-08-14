using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

namespace DefaultNamespace
{
    public class DropdownLanguage : MonoBehaviour
    {
        [SerializeField] private InfoYG _infoYG;
        [SerializeField] private Dropdown _dropdown;
        [SerializeField] private Text _labelText;
        [SerializeField] private Text _itemText;
        
        
        [FormerlySerializedAs("ru")]
        [Header("Translate")]
        [SerializeField] private string[] _Ru = new string[3];
        [FormerlySerializedAs("en")] [SerializeField] private string[] _En = new string[3];
        [FormerlySerializedAs("tr")] [SerializeField] private string[] _Tr = new string[3];
        
        private int _fontNumber = 0;
        private string _languageStart;
        private int _labelBaseFontSize, _itemBaseFontSize;
        
        private void OnEnable()
        {
            //YandexGame.SwitchLangEvent += SwitchLanguage;
            
            switch (YandexGame.lang)
            {
                case "en":
                    _dropdown.value = 0;
                    SwithLanguage(_En, _dropdown.value);
                    break;
                case "ru":
                    _dropdown.value = 1;
                    SwithLanguage(_Ru, _dropdown.value);
                    break;
                case "tr":
                    _dropdown.value = 2;
                    SwithLanguage(_Tr, _dropdown.value);
                    break;
            }
        }

        private void SwithLanguage(string [] language, int index)
        {
            //SwithFont(_infoYG.fonts.tr);
            for (int i = 0; i < language.Length; i++)
                _dropdown.options[i].text = language[i];

            _labelText.text = _dropdown.options[index].text;
        }

        public void InputLanguage(int value)
        {
            switch (value)
            {
                case 0:
                    YandexGame.SwitchLangEvent("en");
                    YandexGame.lang = "en";
                    YandexGame.savesData.language = "en";
                    SwithLanguage(_En, value);
                    break;
                case 1:
                    YandexGame.SwitchLangEvent("ru");
                    YandexGame.lang = "ru";
                    YandexGame.savesData.language = "ru";
                    SwithLanguage(_Ru, value);
                    break;
                case 2:
                    YandexGame.SwitchLangEvent("tr");
                    YandexGame.lang = "tr";
                    YandexGame.savesData.language = "tr";
                    SwithLanguage(_Tr, value);
                    break;
            }
        }
    }
}