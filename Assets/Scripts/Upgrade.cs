using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Upgrade : Item
{
    public int BaseDamage;
    public float AttackSpeed;
    public int Shots;
    public float Size;
    public int HealthBonus;
    public int ShieldBonus;
    public int DefenseBonus;
    public float Speed;   
    public Upgrade(string name, EquipmentType equipmentType, Rarity rarity, int value){
        Name = name;
        EquipmentType = equipmentType;
        Rarity = rarity;
        Value = value;
        Effects = new List<Effect>();
    }
}
