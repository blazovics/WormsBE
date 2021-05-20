using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryT1 : MonoBehaviour
{
    public List<Weapon_Gun> items = new List<Weapon_Gun>();

    public static InventoryT1 instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;

        
    }

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallback;

    public void add(Weapon_Gun item)
    {
        items.Add(item);


        if (onItemChangedCallback != null)
        {
            
            onItemChangedCallback.Invoke();
        }
      

    }

    public void Remove()
    {
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
