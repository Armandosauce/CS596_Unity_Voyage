using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public itemClass itemClass;
    public RG rarity;              //rarityGrade
    public float p;                 //item probability of spawning
    public float value;             //price the item sells for at any shop

    public virtual void Use()
    {

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

public enum RG             //rarityGrade list for all items
{
    Frequent = 0,
    Infrequent = 1,
    Exceptional = 2,
    Extraordinary = 3,
    Fabled = 4
}

public enum itemClass { Consumable, Material, Weapon, Equipment, Charm, Other }
