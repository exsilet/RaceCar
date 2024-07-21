using UnityEngine;
using YG;

namespace DefaultNamespace
{
    public class DropdownLanguage : MonoBehaviour
    {
        public void InputLanguage(int value)
        {
            switch (value)
            {
                case 0:
                    YandexGame.SwitchLangEvent("en");
                    YandexGame.lang = "en";
                    break;
                case 1:
                    YandexGame.SwitchLangEvent("ru");
                    YandexGame.lang = "ru";
                    break;
                case 2:
                    YandexGame.SwitchLangEvent("tr");
                    YandexGame.lang = "tr";
                    break;
            }
        }
    }
}