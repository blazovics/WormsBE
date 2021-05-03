using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryT1 : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

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

    public void add(GameObject item)
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
