
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    private Item item;
    public Image icon;
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
    public void SellItem()
    {
        if (item != null)
        {
            EconomyManager.Instance.AddMoney(item.sellPrice);
            Inventory.instance.Remove(item);
        }
    }
}
