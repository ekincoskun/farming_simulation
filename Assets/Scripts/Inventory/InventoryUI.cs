using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private Transform itemsParent;
    private InventorySlot[] _slots;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.instance;
        _inventory.onItemChangedCallback += UpdateUI;
        
        _slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }
    
    private void UpdateUI()
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
