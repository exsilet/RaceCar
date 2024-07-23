using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cars
{
    [CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
    public class ShopContent : ScriptableObject
    {
        [SerializeField] private List<CarItem> _carItems;
        
        public IEnumerable<CarItem> CarItems => _carItems;

        private void OnValidate()
        {
            var carDuplicates = _carItems.GroupBy(item => item.CarType)
                .Where(array => array.Count() > 1);

            if (carDuplicates.Count() > 0)
                throw new InvalidOperationException(nameof(_carItems));
        }
    }
}