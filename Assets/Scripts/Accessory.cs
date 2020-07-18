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
    public int JumpNumber;
    public Accessory(string name, EquipmentType equipmentType, Rarity rarity, int value, int upgradeSlots, int jumpNumber){
        Name = name;
        EquipmentType = equipmentType;
        Rarity = rarity;
        Value = value;
        JumpNumber = jumpNumber;
        UpgradeSlots = upgradeSlots;
        Upgrades = new List<Item>();
        Effects = new List<Effect>();
    }
}
