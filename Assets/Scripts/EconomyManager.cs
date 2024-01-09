
using UnityEngine;


public class EconomyManager : MonoBehaviour
{
    [SerializeField] private int startingMoney = 100; 
    private int _currentMoney;
    public static EconomyManager Instance;
    public TMPro.TextMeshProUGUI[] moneyText;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        _currentMoney = startingMoney;
        UpdateMoneyText();
    }
    
    public void AddMoney(int amount)
    {
        _currentMoney += amount;
        UpdateMoneyText();
    }
    
    public bool SubtractMoney(int amount)
    {
        if (_currentMoney - amount >= 0)
        {
            _currentMoney -= amount;
            UpdateMoneyText();
            return true;
        }
        else
        {
            Debug.Log("Insufficient funds!");
            return false;
        }
    }
    
    void UpdateMoneyText()
    {
        foreach (var text in moneyText)
        {
            text.text = _currentMoney.ToString();
        }
    }
}