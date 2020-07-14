using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Item
{
    public string Name;
    public string Description;
    public EquipmentType EquipmentType;
    public EquipmentType UpgradeType;
    public Rarity Rarity;
    public int Value;
    public int UpgradeSlots;
    public List<Item> Upgrades = new List<Item>();
    public List<Effect> Effects = new List<Effect>();
    public bool Equipped;

}
