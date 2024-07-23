using System;
using UIExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopCategoryButton : MonoBehaviour
    {
        public event Action Click;

        [SerializeField] private Button _button;

        private void OnEnable() => _button.Add(OnClick);
        private void OnDisable() => _button.Remove(OnClick);

        private void OnClick() => Click?.Invoke();
    }
}