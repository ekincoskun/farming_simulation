using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractable
{
    public Item item;
    public void Interact()
    {
        Debug.Log("Interacted with crop");
        PickUp();
    }
    private void PickUp()
    {
        bool wasPickedUp = Inventory.instance.Add(item);
        Debug.Log(wasPickedUp);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        Inventory.instance.Add(item);
    }
}
