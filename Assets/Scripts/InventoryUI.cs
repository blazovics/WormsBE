using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsparentT1;

    public Transform itemsparentT2;

    InventoryT1 inventoryT1;
    InventoryT2 inventoryT2;

    InventorySlot[] slotsBlue;


    InventorySlot[] slotsYellow;

    // Start is called before the first frame update
    void Start()
    {
        inventoryT1 = InventoryT1.instance;
        inventoryT2 = InventoryT2.instance;

        inventoryT1.onItemChangedCallback += UpdateT1UI;
        inventoryT2.onItemChangedCallback += UpdateT2UI;

        slotsBlue = itemsparentT1.GetComponentsInChildren<InventorySlot>();

        slotsYellow = itemsparentT2.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateT1UI()
    {
        for (int i = 0; i < slotsBlue.Length; i++)
        {
            if (i < inventoryT1.items.Count)
            {
                slotsBlue[i].AddItem(inventoryT1.items[i]);
            }
            else
            {
                slotsBlue[i].DisableSlot();
            }
        }
    }

    void UpdateT2UI()
    {
        for (int i = 0; i < slotsYellow.Length; i++)
        {
            if (i < inventoryT2.items.Count)
            {
                slotsYellow[i].AddItem(inventoryT2.items[i]);
            }
            else
            {
                slotsYellow[i].DisableSlot();
            }
        }
    }



}
