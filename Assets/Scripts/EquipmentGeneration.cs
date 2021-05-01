using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using System.IO;
using System.Reflection;

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
        Player.Armors.Add(GenerateArmor());
        Player.Armors.Add(GenerateArmor());
        Player.Armors.Add(GenerateArmor());
        Player.Accessories.Add(GenerateAccessory());
        Player.Accessories.Add(GenerateAccessory());
        Player.Accessories.Add(GenerateAccessory());
        // Armor anArmor = GenerateArmor();
        // Player.Armor = anArmor;
        // Accessory acc = GenerateAccessory();
        // Player.Accessory = acc;
        // Upgrade upg = GenerateUpgrade();
        Player.AddStatsFromEquipment(EquipmentType.Weapon);
        Player.AddStatsFromEquipment(EquipmentType.Armor);
        Player.AddStatsFromEquipment(EquipmentType.Accessory);       
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
        Armor newArmor = new Armor("testArmor", EquipmentType.Armor, rarity, 100, 10, 1, 0,  2, 0);
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
        Accessory newAccessory = new Accessory("testAccessory", EquipmentType.Accessory, rarity, 100, 1, 0);
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
        newEffect = EG.GenerateEffect(EquipmentType.Upgrade);
        newUpgrade.Effects.Add(newEffect);
        //temporarily for testing
        newUpgrade.UpgradeType = EquipmentType.Weapon;
        newUpgrade.Description += newEffect.Description + " ";
        newUpgrade.Name = newEffect.Name + " Module";
        AddStatFromEffectToUpgrade(newEffect, newUpgrade);
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
        var type = weapon.Attack.GetType();
        var prop = type.GetField(effect.Type);
        if(prop != null){
            var propType = prop.FieldType;
            if(propType == typeof(int)){
                var value = (int)prop.GetValue(weapon.Attack);
                prop.SetValue(weapon.Attack, (int)effect.Bonus + value);
            }
            else{
                var value = (float)prop.GetValue(weapon.Attack);
                prop.SetValue(weapon.Attack, effect.Bonus + value); 
            }
        }
    }
    public void AddStatFromEffectToArmor(Effect effect, Armor armor){
        var type = armor.GetType();
        var prop = type.GetField(effect.Type);
        if(prop != null){
            var propType = prop.FieldType;
            if(propType == typeof(int)){
                var value = (int)prop.GetValue(armor);
                prop.SetValue(armor, (int)effect.Bonus);
            }
            else{
                var value = (float)prop.GetValue(armor);
                prop.SetValue(armor, effect.Bonus); 
            }
        }
    }
    public void AddStatFromEffectToAccessory(Effect effect, Accessory accessory){
        var type = accessory.GetType();
        var prop = type.GetField(effect.Type);
        if(prop != null){
            var propType = prop.FieldType;
            if(propType == typeof(int)){
                var value = (int)prop.GetValue(accessory);
                prop.SetValue(accessory, (int)effect.Bonus);
            }
            else{
                var value = (float)prop.GetValue(accessory);
                prop.SetValue(accessory, effect.Bonus); 
            }
        }
    }
    public void AddStatFromEffectToUpgrade(Effect effect, Upgrade upgrade){
        var type = upgrade.GetType();
        var prop = type.GetField(effect.Type);
        if(prop != null){
            var propType = prop.FieldType;
            if(propType == typeof(int)){
                var value = (int)prop.GetValue(upgrade);
                prop.SetValue(upgrade, (int)effect.Bonus);
            }
            else{
                var value = (float)prop.GetValue(upgrade);
                prop.SetValue(upgrade, effect.Bonus); 
            }
        }
    }
    #endregion
}
