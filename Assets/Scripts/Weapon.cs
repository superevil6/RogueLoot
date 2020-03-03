using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Weapon
{
    public string Name;
    public Rarity Rarity;
    public int Value;
    public Attack Attack;
    public int UpgradeSlots;
    public List<Upgrade> Upgrades = new List<Upgrade>();
    public List<Effect> Effects = new List<Effect>();

    public Weapon(string name, Rarity rarity, int value, Attack attack, int upgradeSlots){
        Name = name;
        Rarity = rarity;
        Value = value;
        Attack = attack;
        UpgradeSlots = upgradeSlots;
        Upgrades = new List<Upgrade>();
        Effects = new List<Effect>();
    }
}
