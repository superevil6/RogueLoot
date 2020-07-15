using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Enums;

[System.Serializable]
public class EffectGeneration : MonoBehaviour
{
    public Effect EffectManager;
    public Effects Effects;
    public List<Effect> WeaponEffects = new List<Effect>();
    public List<Effect> ArmorEffects = new List<Effect>();
    public List<Effect> AccessoryEffects = new List<Effect>();
    List<Effect> LevelAndTypeSpecificEffects = new List<Effect>();

    private string jsonPath;
    private string jsonString;

    void Start()
    {
        jsonPath = Application.dataPath+"/StreamingAssets/effects.json";
        jsonString = File.ReadAllText(jsonPath);
        Effects = CreateFromJson(jsonString);
        WeaponEffects = ReturnEquipmentSpecificEffects("Weapon");
        ArmorEffects = ReturnEquipmentSpecificEffects("Armor");
        AccessoryEffects = ReturnEquipmentSpecificEffects("Accessory");
    }

    public Effects CreateFromJson(string jsonString){
        return JsonUtility.FromJson<Effects>(jsonString);
    }

    public List<Effect> ReturnEquipmentSpecificEffects(string EquipmentType){
        List<Effect> specificEffects = new List<Effect>();
        foreach(Effect effect in Effects.EffectList){
            if(effect.EquipmentType.Contains(EquipmentType)){
                specificEffects.Add(effect);
            }
        }
        return specificEffects;
    }

    public Effect GenerateEffect(EquipmentType ET){
        Effect effect;
        int effectIndex;
        switch(ET){
            case EquipmentType.Weapon:
            effectIndex = Random.Range(0, WeaponEffects.Count);
            effect = CheckForWeaponEffects(WeaponEffects[effectIndex]);
            return effect;
            case EquipmentType.Armor:
            effectIndex = Random.Range(0, ArmorEffects.Count);
            effect = CheckForArmorEffects(ArmorEffects[effectIndex]);
            return effect;
            case EquipmentType.Accessory:
            effectIndex = Random.Range(0, AccessoryEffects.Count);
            effect = CheckForAccessoryEffects(AccessoryEffects[effectIndex]);
            return effect;
            default:
            return null;
        }
    }
    public Effect GenerateEffect(EquipmentType ET, Rarity rarity){
        LevelAndTypeSpecificEffects.Clear();
        foreach(Effect effect in Effects.EffectList){
            if(effect.EquipmentType == ET.ToString() && effect.Rarity == rarity.ToString()){
                LevelAndTypeSpecificEffects.Add(effect);
            }
        }
        Effect newEffect;
        int effectIndex;
        switch(ET){
            case EquipmentType.Weapon:
            effectIndex = Random.Range(0, WeaponEffects.Count);
            newEffect = CheckForWeaponEffects(WeaponEffects[effectIndex]);
            return newEffect;
            case EquipmentType.Armor:
            effectIndex = Random.Range(0, ArmorEffects.Count);
            newEffect = CheckForArmorEffects(ArmorEffects[effectIndex]);
            return newEffect;
            case EquipmentType.Accessory:
            effectIndex = Random.Range(0, AccessoryEffects.Count);
            newEffect = CheckForAccessoryEffects(AccessoryEffects[effectIndex]);
            return newEffect;
            case EquipmentType.Upgrade:
            effectIndex = Random.Range(0, AccessoryEffects.Count);
            newEffect = CheckForAccessoryEffects(AccessoryEffects[effectIndex]);
            return newEffect;
            default:
            return null;
        }
    }

    #region CheckingMethods

    public Effect CheckForWeaponEffects(Effect effect){
        if(effect.LowerPowerBonus != 0){
            effect.PowerBonus = SetRandomValues(effect.LowerPowerBonus, effect.UpperPowerBonus);
            effect.Description = "Damage +" + effect.PowerBonus + "\n";
        }
        if(effect.LowerBulletSizeBonus != 0){
            effect.BulletSizeBonus = SetRandomValues(effect.LowerBulletSizeBonus, effect.UpperBulletSizeBonus);
            effect.Description = "Bullet Size +" + effect.BulletSizeBonus + "\n";
        }
        if(effect.LowerAttackSpeedBonus != 0){
            effect.AttackSpeedBonus = SetRandomValues(effect.LowerAttackSpeedBonus, effect.UpperAttackSpeedBonus);
            effect.Description = "Attack Speed +" + effect.AttackSpeedBonus + "\n";
        }
        if(effect.LowerBulletSpeedBonus != 0){
            effect.BulletSpeedBonus = SetRandomValues(effect.LowerBulletSpeedBonus, effect.UpperBulletSpeedBonus);
            effect.Description = "Bullet Speed +" + effect.BulletSpeedBonus + "\n";
        }
        if(effect.BulletCountBonus != 0){
            effect.Description = "Bullets Fired +" + effect.BulletCountBonus + "\n";
        }
        if(effect.AccuracyBonus != 0){
            effect.AccuracyBonus = SetRandomValues(effect.LowerAccuracyBonus, effect.UpperAccuracyBonus);
            effect.Description = "Bullet Accuracy +" + Mathf.Round(effect.AccuracyBonus) + "%\n";
        }
        if(effect.LowerLuckBonus != 0){
            effect.LuckBonus = SetRandomValues(effect.LowerLuckBonus, effect.UpperLuckBonus);
        }
        if(effect.LowerColdDamageBonus != 0){
            effect.ColdBonus = SetRandomValues(effect.LowerColdDamageBonus, effect.UpperColdDamageBonus);
            effect.Description = "Chilling Damage +" + effect.ColdBonus + "\n";
        }
        if(effect.LowerFireDamageBonus != 0){
            effect.FireBonus = SetRandomValues(effect.LowerFireDamageBonus, effect.UpperFireDamageBonus);
            effect.Description = "Flaming Damage +" + effect.FireBonus + "\n";
        }
        if(effect.LowerElectricDamageBonus != 0){
            effect.ElectricBonus = SetRandomValues(effect.LowerElectricDamageBonus, effect.UpperElectricDamageBonus);
            effect.Description = "Electical Damage +" + effect.ElectricBonus + "\n";
        }
        if(effect.LowerPoisonDamageBonus != 0){
            effect.PoisonBonus = SetRandomValues(effect.LowerPoisonDamageBonus, effect.UpperPoisonDamageBonus);
            effect.Description = "Poisoning Damage +" + effect.PoisonBonus + "\n";
        }
        return effect;
    }
    public Effect CheckForAccessoryEffects(Effect effect){
        if(effect.LowerPowerBonus != 0){
            effect.PowerBonus = SetRandomValues(effect.LowerPowerBonus, effect.UpperPowerBonus);
            effect.Description = "Damage +" + effect.PowerBonus + "\n";
        }
        if(effect.LowerBulletSizeBonus != 0){
            effect.BulletSizeBonus = SetRandomValues(effect.LowerBulletSizeBonus, effect.UpperBulletSizeBonus);
            effect.Description = "Bullet Size +" + Mathf.Round(effect.BulletSizeBonus) + "&\n";
        }
        if(effect.LowerAttackSpeedBonus != 0){
            effect.AttackSpeedBonus = SetRandomValues(effect.LowerAttackSpeedBonus, effect.UpperAttackSpeedBonus);
            effect.Description = "Attack Speed " + Mathf.Round(effect.AttackSpeedBonus) + "%\n";
        }
        if(effect.LowerBulletSpeedBonus != 0){
            effect.BulletSpeedBonus = SetRandomValues(effect.LowerBulletSpeedBonus, effect.UpperBulletSpeedBonus);
            effect.Description = "Bullet Speed +" + effect.BulletSpeedBonus + "%\n";
        }
        if(effect.BulletCountBonus != 0){
            effect.Description = "Bullets Fired +" + effect.BulletCountBonus + "\n";
        }
        if(effect.LowerLuckBonus != 0){
            effect.LuckBonus = SetRandomValues(effect.LowerLuckBonus, effect.UpperLuckBonus);
            effect.Description = "Luck +" + effect.LuckBonus + "\n";
        }
        if(effect.LowerDefenseBonus != 0){
            effect.DefenseBonus = SetRandomValues(effect.LowerDefenseBonus, effect.UpperDefenseBonus);
            effect.Description = "Defense +" + effect.DefenseBonus + "\n";
        }
        if(effect.LowerHealthBonus != 0){
            effect.HealthBonus = SetRandomValues(effect.LowerHealthBonus, effect.UpperHealthBonus);
            effect.Description = "Health +" + effect.HealthBonus + "\n";
        }
        if(effect.LowerShieldBonus != 0){
            effect.ShieldBonus = SetRandomValues(effect.LowerShieldBonus, effect.UpperShieldBonus);
            effect.Description = "Shield +" + effect.ShieldBonus + "\n";
        }
        if(effect.LowerPlayerSpeedBonus != 0){
            effect.PlayerSpeedBonus = SetRandomValues(effect.LowerPlayerSpeedBonus, effect.UpperPlayerSpeedBonus);
            effect.Description = "Movement Speed +" + effect.PlayerSpeedBonus + "\n";
        }
        if(effect.AccuracyBonus != 0){
            effect.AccuracyBonus = SetRandomValues(effect.LowerAccuracyBonus, effect.UpperAccuracyBonus);
            effect.Description = "Bullet Accuracy +" + Mathf.Round(effect.AccuracyBonus) + "%\n";
        }
        return effect;
    }
    public Effect CheckForArmorEffects(Effect effect){
        if(effect.LowerDefenseBonus != 0){
            effect.DefenseBonus = SetRandomValues(effect.LowerDefenseBonus, effect.UpperDefenseBonus);
            effect.Description = "Defense +" + effect.DefenseBonus + "\n";
        }
        if(effect.LowerHealthBonus != 0){
            effect.HealthBonus = SetRandomValues(effect.LowerHealthBonus, effect.UpperHealthBonus);
            effect.Description = "Health +" + effect.HealthBonus + "\n";
        }
        if(effect.LowerShieldBonus != 0){
            effect.ShieldBonus = SetRandomValues(effect.LowerShieldBonus, effect.UpperShieldBonus);
            effect.Description = "Shield +" + effect.ShieldBonus + "\n";
        }
        if(effect.LowerPlayerSpeedBonus != 0){
            effect.PlayerSpeedBonus = SetRandomValues(effect.LowerPlayerSpeedBonus, effect.UpperPlayerSpeedBonus);
            effect.Description = "Movement Speed +" + effect.PlayerSpeedBonus + "\n";
        }
        if(effect.LowerLuckBonus != 0){
            effect.LuckBonus = SetRandomValues(effect.LowerLuckBonus, effect.UpperLuckBonus);
            effect.Description = "Luck +" + effect.LuckBonus + "\n";
        }
        return effect;
    }
    #endregion

    #region Utility Methods

    public int SetRandomValues(int valueOne, int valueTwo){
        return Random.Range(valueOne, valueTwo);
    }
    public float SetRandomValues(float valueOne, float valueTwo){
        return Random.Range(valueOne, valueTwo);
    }

    #endregion
}
