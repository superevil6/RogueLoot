using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using System.IO;

public class EquipmentGeneration : MonoBehaviour
{
    public EffectGeneration EG;
    public Player Player;
    public List<Attack> Attacks = new List<Attack>();
    void Start()
    {
        Player.Weapons.Add(GenerateWeapon());
        Player.Weapons.Add(GenerateWeapon());
        Player.Weapons.Add(GenerateWeapon());
        Player.Weapons.Add(GenerateWeapon());

        Armor anArmor = GenerateArmor();
        Player.Armor = anArmor;
        Accessory acc = GenerateAccessory();
        Player.Accessory = acc;
        Upgrade upg = GenerateUpgrade();
        // aWeapon.Upgrades.Add(upg);
        // Player.Inventory.Add(aWeapon);
    }

    #region ItemCreation
        public Weapon GenerateWeapon(){
            Rarity rarity = GenerateRarity(Player.Luck);
            Weapon newWeapon = new Weapon("testGun", EquipmentType.Weapon, rarity, 100, FindAttack(Attacks), 2);
            int effectsToAdd = 0;
            switch(newWeapon.Rarity){
                case Rarity.Legendary:
                effectsToAdd = 4;
                break;
                case Rarity.Rare:
                effectsToAdd = 3;
                break;
                case Rarity.Magical:
                effectsToAdd = 2;
                break;
                case Rarity.Normal:
                effectsToAdd = 1;
                break;
            }
            Effect newEffect;
            for(int i = effectsToAdd; i > 0; i--){
                newEffect = EG.GenerateEffect(EquipmentType.Weapon);
                newWeapon.Effects.Add(newEffect);
                newWeapon.Description += newEffect.Description + " ";
                AddStatFromEffectToWeapon(newEffect, newWeapon);
            }
            newWeapon.Name = newWeapon.Attack.AttackName + " of " + newWeapon.Effects[0].Name;
            return newWeapon;
        }

    public Armor GenerateArmor(){
        Rarity rarity = GenerateRarity(Player.Luck);
        Armor newArmor = new Armor("testArmor", EquipmentType.Armor, rarity, 100, 10, 1, 0,  2);
        int effectsToAdd = 0;
        switch(newArmor.Rarity){
            case Rarity.Legendary:
            effectsToAdd = 4;
            break;
            case Rarity.Rare:
            effectsToAdd = 3;
            break;
            case Rarity.Magical:
            effectsToAdd = 2;
            break;
            case Rarity.Normal:
            effectsToAdd = 1;
            break;
        }
        Effect newEffect;
        for(int i = effectsToAdd; i > 0; i--){
            newEffect = EG.GenerateEffect(EquipmentType.Armor);
            newArmor.Effects.Add(newEffect);
            newArmor.Description += newEffect.Description + " ";
            AddStatFromEffectToArmor(newEffect, newArmor);
        }
        newArmor.Name = "Armor" + " of " + newArmor.Effects[0].Name;

        return newArmor;
    }
    public Accessory GenerateAccessory(){
        Rarity rarity = GenerateRarity(Player.Luck);
        Accessory newAccessory = new Accessory("testAccessory", EquipmentType.Accessory, rarity, 100, 1);
        int effectsToAdd = 0;
        switch(newAccessory.Rarity){
            case Rarity.Legendary:
            effectsToAdd = 4;
            break;
            case Rarity.Rare:
            effectsToAdd = 3;
            break;
            case Rarity.Magical:
            effectsToAdd = 2;
            break;
            case Rarity.Normal:
            effectsToAdd = 1;
            break;
        }
        Effect newEffect;
        for(int i = effectsToAdd; i > 0; i--){
            newEffect = EG.GenerateEffect(EquipmentType.Accessory);
            newAccessory.Effects.Add(newEffect);
            newAccessory.Description += newEffect.Description + " ";
            AddStatFromEffectToAccessory(newEffect, newAccessory);
        }
        newAccessory.Name = "Ring" + " of " + newAccessory.Effects[0].Name;

        return newAccessory;
    }
    public Upgrade GenerateUpgrade(){
        Rarity rarity = GenerateRarity(Player.Luck);
        Upgrade newUpgrade = new Upgrade("testUpgrade", EquipmentType.Upgrade, rarity, 100);
        Effect newEffect;
        newEffect = EG.GenerateEffect(EquipmentType.Upgrade, newUpgrade.Rarity);
        print(newEffect + "new effect");
        newUpgrade.Effects.Add(newEffect);
        //temporarily for testing
        newUpgrade.UpgradeType = EquipmentType.Weapon;
        // newUpgrade.Description += newEffect.Description + " ";
        AddStatFromEffectToUpgrade(newEffect, newUpgrade);
        newUpgrade.Name = newEffect.Name + " Module";
        return newUpgrade;
    }
    #endregion

    #region Rarity
        public Rarity GenerateRarity(int luck){
            int roll = Random.Range(0, 100 - luck);
            if(roll <= 5){
                return Rarity.Legendary;
            }
            if(roll > 5 && roll <= 15){
                return Rarity.Rare;
            }
            if(roll > 15 && roll <= 33){
                return Rarity.Magical;
            }
            else{
                return Rarity.Normal;
            }
        }
    #endregion
    #region FindAttacks
        public Attack FindAttack(List<Attack> Attacks){
            Attack Attack = Instantiate(Attacks[Random.Range(0, Attacks.Count)]);
            return Attack;
        }
    #endregion
    #region AddEffectStats
    public void AddStatFromEffectToWeapon(Effect effect, Weapon weapon){
        weapon.Attack.BaseDamage += effect.PowerBonus;
        weapon.Attack.AttackSpeed += effect.AttackSpeedBonus;
        weapon.Attack.Shots += effect.BulletCountBonus;
        weapon.Attack.Size += effect.BulletSizeBonus;
        weapon.Attack.Accuracy += effect.AccuracyBonus;
        weapon.Attack.ColdDamage += effect.ColdBonus;
        weapon.Attack.FireDamage += effect.FireBonus;
        weapon.Attack.ElectricDamage += effect.ElectricBonus;
        weapon.Attack.PoisonDamage += effect.PoisonBonus;
    }
    public void AddStatFromEffectToArmor(Effect effect, Armor armor){
        armor.HealthBonus += effect.HealthBonus;
        armor.ShieldBonus += effect.ShieldBonus;
        armor.DefenseBonus += effect.DefenseBonus;
        armor.Speed += effect.PlayerSpeedBonus;
    }
    public void AddStatFromEffectToAccessory(Effect effect, Accessory accessory){
        accessory.BaseDamage += effect.PowerBonus;
        accessory.AttackSpeed += effect.AttackSpeedBonus;
        accessory.Shots += effect.BulletCountBonus;
        accessory.Size += effect.BulletSizeBonus;
        accessory.HealthBonus += effect.HealthBonus;
        accessory.ShieldBonus += effect.ShieldBonus;
        accessory.DefenseBonus += effect.DefenseBonus;
        accessory.Speed += effect.PlayerSpeedBonus;
        accessory.AccuracyBonus += effect.AccuracyBonus;

    }
    public void AddStatFromEffectToUpgrade(Effect effect, Upgrade upgrade){
        upgrade.BaseDamage += effect.PowerBonus;
        upgrade.AttackSpeed += effect.AttackSpeedBonus;
        upgrade.Shots += effect.BulletCountBonus;
        upgrade.Size += effect.BulletSizeBonus;
        upgrade.HealthBonus += effect.HealthBonus;
        upgrade.ShieldBonus += effect.ShieldBonus;
        upgrade.DefenseBonus += effect.DefenseBonus;
        upgrade.Speed += effect.PlayerSpeedBonus;
    }
    #endregion
}
