using System;
using Cars;
using SaveData;
using UI;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private CarViewShop _carViewShopPrefab;
    
    private int _price;

    public CarViewShop Get(CarStaticData data, Transform parent, SaveLoadService saveLoadService)
    {
        CarViewShop instance;

        switch (data)
        {
            case CarItem carViewShop:
                instance = Instantiate(_carViewShopPrefab, parent);
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(instance));
        }
        
        ReadePrice(data, saveLoadService);
        instance.Initialize(data, _price, saveLoadService);
        return instance;
    }
    
    private int ReadePrice(CarStaticData data, SaveLoadService saveLoad)
    {
        if (data.Level > 0) 
            _price = saveLoad.ReadPriceCar(data.Level.ToString());

        if (_price == 0) 
            _price = data.StartPrice;
        
        return _price;
    }
}