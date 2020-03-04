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
    // Start is called before the first frame update
    void Start()
    {
        Weapon aWeapon = GenerateWeapon();
        Player.Weapon = aWeapon;
        Armor anArmor = GenerateArmor();
        Player.Armor = anArmor;
        Player.AddStatsFromEquipment(EquipmentType.Armor);
        Player.Inventory.Add(aWeapon);
        Player.Inventory.Add(GenerateWeapon());
        Player.Inventory.Add(GenerateArmor());
        Player.Inventory.Add(anArmor);
        print(JsonUtility.ToJson(anArmor));
        // print(JsonUtility.ToJson(aWeapon));
    }
    // Update is called once per frame

    #region ItemCreation
        public Weapon GenerateWeapon(){
            Rarity rarity = GenerateRarity(Player.Luck);
            Weapon newWeapon = new Weapon("testGun", EquipmentType.Weapon, rarity, 100, FindAttack(Attacks), 0);
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
        Armor newArmor = new Armor("testArmor", EquipmentType.Armor, rarity, 100, 10, 1, 0,  0);
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
        return newArmor;
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
        }
        public void AddStatFromEffectToArmor(Effect effect, Armor armor){
            armor.HealthBonus += effect.HealthBonus;
            armor.ShieldBonus += effect.ShieldBonus;
            armor.DefenseBonus += effect.DefenseBonus;
            armor.Speed += effect.PlayerSpeedBonus;
        }
    #endregion
}
