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
    public int PlayerSpeedBonus;
    public int LowerPlayerSpeedBonus;
    public int UpperPlayerSpeedBonus;
    public int BulletCountBonus;
    public float BulletSizeBonus;
    public float LowerBulletSizeBonus;
    public float UpperBulletSizeBonus;
    public float AttackSpeedBonus;
    public float UpperAttackSpeedBonus;
    public float LowerAttackSpeedBonus;
    public float BulletSpeedBonus;
    public float LowerBulletSpeedBonus;
    public float UpperBulletSpeedBonus;
    public float AccuracyBonus;
    public float LowerAccuracyBonus;
    public float UpperAccuracyBonus;
    //Elemental
    public int ColdBonus;
    public int LowerColdDamageBonus;
    public int UpperColdDamageBonus;
    public int FireBonus;
    public int LowerFireDamageBonus;
    public int UpperFireDamageBonus;
    public int ElectricBonus;
    public int LowerElectricDamageBonus;
    public int UpperElectricDamageBonus;
    public int PoisonBonus;
    public int LowerPoisonDamageBonus;
    public int UpperPoisonDamageBonus;

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
    int luckBonus, int lowerLuckBonus, int upperLuckBonus,
    int playerSpeedBonus, int lowerPlayerSpeedBonus, int upperPlayerSpeedBonus,
    int bulletCountBonus, 
    float bulletSizeBonus, float lowerBulletSizeBonus, float upperBulletSizeBonus,
    float attackSpeedBonus, float lowerAttackSpeedBonus, float upperAttackSpeedBonus,
    float bulletSpeedBonus, float lowerBulletSpeedBonus, float upperBulletSpeedBonus,
    float accuracyBonus, float lowerAccuracyBonus, float upperAccuracyBonus,
    int coldBonus, int lowerColdDamageBonus, int upperColdDamageBonus, 
    int fireBonus, int lowerFireDamageBonus, int upperFireDamageBonus, 
    int electricBonus, int lowerElectricDamageBonus, int upperElectricDamageBonus,
    int poisonBonus, int lowerPoisonDamageBonus, int upperPoisonDamageBonus
    ){
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
        PlayerSpeedBonus = playerSpeedBonus;
        LowerPlayerSpeedBonus = lowerPlayerSpeedBonus;
        UpperPlayerSpeedBonus = upperPlayerSpeedBonus;
        BulletCountBonus = bulletCountBonus;
        BulletSizeBonus = bulletSizeBonus;
        LowerBulletSizeBonus = lowerBulletSizeBonus;
        UpperBulletSizeBonus = upperBulletSizeBonus;
        AttackSpeedBonus = attackSpeedBonus;
        LowerAttackSpeedBonus = lowerAttackSpeedBonus;
        UpperAttackSpeedBonus = upperAttackSpeedBonus;
        BulletSpeedBonus = bulletSpeedBonus;
        LowerBulletSpeedBonus = lowerBulletSpeedBonus;
        UpperBulletSpeedBonus = upperBulletSpeedBonus;
        AccuracyBonus = accuracyBonus;
        LowerAccuracyBonus = lowerAccuracyBonus;
        UpperAccuracyBonus = upperAccuracyBonus;
        //Elemental Effects
        ColdBonus = coldBonus;
        LowerColdDamageBonus = lowerColdDamageBonus;
        UpperColdDamageBonus = upperColdDamageBonus;
        FireBonus = fireBonus;
        LowerFireDamageBonus = lowerFireDamageBonus;
        UpperFireDamageBonus = upperFireDamageBonus;
        ElectricBonus = electricBonus;
        LowerElectricDamageBonus = lowerElectricDamageBonus;
        UpperElectricDamageBonus = upperElectricDamageBonus;
        PoisonBonus = LowerPoisonDamageBonus;
        LowerPoisonDamageBonus = lowerPoisonDamageBonus;
        UpperPoisonDamageBonus = upperPoisonDamageBonus;
    }
}


[System.Serializable]
public class Effects{
    public List<Effect> EffectList;
}