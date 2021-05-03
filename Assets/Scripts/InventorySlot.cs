using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;

    GameObject item;

    public Button button;

    public void AddItem(GameObject newItem)
    {
        item = newItem;

        //icon.sprite = item.icon;
        icon.enabled = true;

    }

    public void DisableSlot()
    {
        button.interactable = false;
    }

    public void UseItem()
    {
        if (item != null)
        {
            //item.Use();
        }
    }


}
