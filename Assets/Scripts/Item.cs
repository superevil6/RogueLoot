using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Item
{
    public string Name;
    public string Description;
    public EquipmentType EquipmentType;
    public Rarity Rarity;
    public int Value;
    public int UpgradeSlots;
    public List<Upgrade> Upgrades = new List<Upgrade>();
    public List<Effect> Effects = new List<Effect>();
}
