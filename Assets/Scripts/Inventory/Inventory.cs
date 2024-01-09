using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
        
    }
    #endregion
    
    public Action onItemChangedCallback;
    public int space = 15;
    public List<Item> items = new List<Item>();
    
    public bool Add(Item item)
    {
        if (!item.isDefaultItem&&items.Count<=space)
        {
            Debug.Log("Add item: "+item.name);
            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            return true;
        }else return false;
    }
    public bool Remove(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            return true;
        }else return false;
    }
}
