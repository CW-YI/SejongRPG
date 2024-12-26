using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items;
    [SerializeField] public List<Slot> slots;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);

        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Count; i++) { slots[i].item = items[i]; }
        for (; i < slots.Count; i++) { slots[i].item = null; }
    }

    public void AddItem(Item _item)
    {
        if (items.Count < slots.Count)
        {
            items.Add(_item);
            FreshSlot();
        }
        else Debug.Log("slot is full");
    }

    public bool FindItem(Item _item)
    {
        if (items.Contains(_item)) return true;
        return false;
    }

    public void UseItem(Item _item)
    {
        if (items.Contains(_item))
        {
            Debug.Log(_item.name);
            items.Remove(_item);
            FreshSlot();
        }
        else Debug.Log("item is null");
    }

    public void ClearInventory()
    {
        items.Clear();

        foreach (var item in slots)
        {
            item.item = null;
        }
        FreshSlot();
    }
}
