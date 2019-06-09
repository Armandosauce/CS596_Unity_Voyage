using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class Equipment : Item
{

    public EquipmentSlot equipSlot;

    public int armorMod;
    public int damageMod;
    public Sprite idle;
    public Animator anim;

    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();

    }

}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }