using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Armor : Item
{
    public int DefenseBonus;
    public int HealthBonus;
    public int ShieldBonus;
    public float Speed;

    public Armor(string name, EquipmentType equipmentType, Rarity rarity, int value, int healthBonus, int defenseBonus, int shieldBonus, int upgradeSlots){
        Name = name;
        EquipmentType = equipmentType;
        Rarity = rarity;
        Value = value;
        HealthBonus = healthBonus;
        DefenseBonus = defenseBonus;
        ShieldBonus = shieldBonus;
        UpgradeSlots = upgradeSlots;
        Upgrades = new List<Item>();
        Effects = new List<Effect>();
    }
}
