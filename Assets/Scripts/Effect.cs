using System.Collections;
using System.Collections.Generic;
using Enums;

[System.Serializable]
public class Effect
{
    public string Name;
    public string EquipmentType;
    public string Rarity;
    public int HealthBonus;
    public int LowerHealthBonus;
    public int UpperHealthBonus;
    public int ShieldBonus;
    public int LowerShieldBonus;
    public int UpperShieldBonus;
    public int PowerBonus;
    public int LowerPowerBonus;
    public int UpperPowerBonus;
    public int DefenseBonus;
    public int LowerDefenseBonus;
    public int UpperDefenseBonus;
    public int LuckBonus;
    public int LowerLuckBonus;
    public int UpperLuckBonus;
    /*
    range
    bullet size
    bullet count
    range
    attack speed
    bullet speed
    burning damage
    freezing chance
    sheild depleting
    chain lightning
    */



    public Effect(
    string name, string equipmentType, string rarity,
    int healthBonus, int lowerHealthBonus, int upperHealthBonus, 
    int shieldBonus, int lowerShieldBonus, int upperShieldBonus, 
    int powerBonus, int lowerPowerBonus, int upperPowerBonus,
    int defenseBonus, int lowerDefenseBonus, int upperDefenseBonus, 
    int luckBonus, int lowerLuckBonus, int upperLuckBonus){
        Name = name;
        EquipmentType =  equipmentType;
        Rarity = rarity;
        HealthBonus = healthBonus;
        LowerHealthBonus = lowerHealthBonus;
        UpperHealthBonus = upperHealthBonus;
        ShieldBonus = shieldBonus;
        LowerShieldBonus = lowerShieldBonus;
        UpperShieldBonus = upperShieldBonus;
        PowerBonus = powerBonus;
        LowerPowerBonus = lowerPowerBonus;
        UpperPowerBonus = upperPowerBonus;
        DefenseBonus = defenseBonus;
        LowerDefenseBonus = lowerDefenseBonus;
        UpperDefenseBonus = upperDefenseBonus;
        LuckBonus = luckBonus;
        LowerLuckBonus = lowerLuckBonus;
        UpperLuckBonus = upperLuckBonus;
    }
}


[System.Serializable]
public class Effects{
    public List<Effect> EffectList;
}