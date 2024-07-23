using System.Linq;
using Cars;
using UnityEngine;

namespace UI
{
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private ShopContent _contentItems;
        [SerializeField] private ShopCategoryButton _carButton;
        [SerializeField] private ShopPanel _shopPanel;

        private void OnEnable() => _carButton.Click += OnClick;

        private void OnClick()
        {
            _shopPanel.Show(_contentItems.CarItems.Cast<CarItem>());
        }

        private void OnDisable() => _carButton.Click -= OnClick;
    }
}