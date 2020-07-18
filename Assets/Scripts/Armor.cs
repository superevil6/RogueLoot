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
    public int JumpNumber;

    public Armor(string name, EquipmentType equipmentType, Rarity rarity, int value, int healthBonus, int defenseBonus, int shieldBonus, int upgradeSlots, int jumpNumber){
        Name = name;
        EquipmentType = equipmentType;
        Rarity = rarity;
        Value = value;
        HealthBonus = healthBonus;
        DefenseBonus = defenseBonus;
        ShieldBonus = shieldBonus;
        JumpNumber = jumpNumber;
        UpgradeSlots = upgradeSlots;
        
        Upgrades = new List<Item>();
        Effects = new List<Effect>();
    }
}
