using System.Collections;
using System.Collections.Generic;
using Enums;

[System.Serializable]
public class Effect
{
    public string Name;
    public string EquipmentType;
    public string Level;
    public string Rarity;
    public string Description;
    public string Type;
    public float Bonus;
    public float Upper;
    public float Lower;
}


[System.Serializable]
public class Effects{
    public List<Effect> EffectList;
}