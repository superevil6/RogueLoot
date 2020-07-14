using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Accessory : Item
{
    public int BaseDamage;
    public float AttackSpeed;
    public int Shots;
    public float Size;
    public int HealthBonus;
    public int ShieldBonus;
    public int DefenseBonus;
    public float Speed;   
    public float AccuracyBonus;
    public Accessory(string name, EquipmentType equipmentType, Rarity rarity, int value, int upgradeSlots){
        Name = name;
        EquipmentType = equipmentType;
        Rarity = rarity;
        Value = value;
        UpgradeSlots = upgradeSlots;
        Upgrades = new List<Item>();
        Effects = new List<Effect>();
    }
}
