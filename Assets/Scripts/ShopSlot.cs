using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private EconomyManager economyManager;
    public void BuyItem()
    {
        if (economyManager.SubtractMoney(item.buyPrice))
        {
            Inventory.instance.Add(item);
            Debug.Log(item.name + " bought");
        }
            
        
    }
}
