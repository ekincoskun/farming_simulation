using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;

    private void Start()
    {
        shopUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.GetComponentInChildren<ShopUI>().UpdateShopUI();
            shopUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(false);
        }
    }
}
