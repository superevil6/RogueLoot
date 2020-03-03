using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Armor
{
    public string Name;
    public Rarity Rarity;
    public int Value;
    public int UpgradeSlots;
    public int DefenseBonus;
    public int HealthBonus;
    public int ShieldBonus;
    public List<Upgrade> Upgrades = new List<Upgrade>();
    public List<Effect> Effects = new List<Effect>();

    public Armor(string name, Rarity rarity, int value, int healthBonus, int defenseBonus, int shieldBonus, int upgradeSlots){
        Name = name;
        Rarity = rarity;
        Value = value;
        HealthBonus = healthBonus;
        DefenseBonus = defenseBonus;
        ShieldBonus = shieldBonus;
        UpgradeSlots = upgradeSlots;
        Upgrades = new List<Upgrade>();
        Effects = new List<Effect>();
    }
}
