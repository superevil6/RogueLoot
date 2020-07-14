using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Weapon : Item
{

    public Attack Attack;


    public Weapon(string name, EquipmentType equipmentType, Rarity rarity, int value, Attack attack, int upgradeSlots){
        Name = name;
        EquipmentType = equipmentType;
        Rarity = rarity;
        Value = value;
        Attack = attack;
        UpgradeSlots = upgradeSlots;
        Upgrades = new List<Item>();
        Effects = new List<Effect>();
    }
}
