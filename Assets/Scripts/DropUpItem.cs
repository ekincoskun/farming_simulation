using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropUpItem : MonoBehaviour,IInteractable
{
    public Item item;
    public void Interact()
    {
        DropUp();
    }
    private void DropUp()
    {
        if (gameObject.transform.childCount==0)
        {
            Inventory.instance.Remove(item);
            var farmLand = GetComponent<FarmLand>();
            farmLand.PlantCrop();
        }
    }
}
