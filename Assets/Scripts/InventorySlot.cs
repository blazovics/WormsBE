using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;

    Weapon_Gun item;

    public Button button;

     

    public void AddItem(Weapon_Gun newItem)
    {
        item = newItem;
        button.interactable = true;

        icon.sprite = item.icon;
        
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
            for (int i = 0; i < RoundManager.singleton.worms.Length; i++)
            {
                
                if (RoundManager.singleton.IsMyTurn(RoundManager.singleton.worms[i].wormId))
                {
                    if (RoundManager.singleton.worms[i].gun == null)
                    {
                        item.gameObject.SetActive(true);
                        item.gameObject.transform.position = new Vector3(RoundManager.singleton.worms[i].gameObject.transform.position.x, RoundManager.singleton.worms[i].gameObject.transform.position.y - 0.15f, RoundManager.singleton.worms[i].gameObject.transform.position.z);
                        item.gameObject.transform.SetParent(RoundManager.singleton.worms[i].gameObject.transform);
                        RoundManager.singleton.worms[i].gun = item.gameObject;
                        RoundManager.singleton.worms[i].inventoryUI.SetActive(false);
                        Cursor.visible = false;
                    }
                    else
                    {
                        RoundManager.singleton.worms[i].gun.SetActive(false);
                        item.gameObject.transform.position = new Vector3(RoundManager.singleton.worms[i].gameObject.transform.position.x, RoundManager.singleton.worms[i].gameObject.transform.position.y - 0.15f, RoundManager.singleton.worms[i].gameObject.transform.position.z);
                        item.gameObject.transform.SetParent(RoundManager.singleton.worms[i].gameObject.transform);
                        RoundManager.singleton.worms[i].gun = item.gameObject;
                        RoundManager.singleton.worms[i].gun.SetActive(true);
                        RoundManager.singleton.worms[i].inventoryUI.SetActive(false);
                        Cursor.visible = false;
                    }
                    

                }
            }
            
        }
    }


}
