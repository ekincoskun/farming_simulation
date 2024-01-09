using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private Transform itemsParent;
    private InventorySlot[] _slots;
    void Start()
    {
        _inventory=Inventory.instance;
        _inventory.onItemChangedCallback += UpdateShopUI;
        _slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateShopUI()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i<_inventory.items.Count)
            {
                _slots[i].AddItem(_inventory.items[i]);
            }
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}
