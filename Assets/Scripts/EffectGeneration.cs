﻿using System.Collections;
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

    private string jsonPath;
    private string jsonString;

    void Start()
    {
        jsonPath = Application.dataPath+"/StreamingAssets/effects.json";
        jsonString = File.ReadAllText(jsonPath);
        Effects = CreateFromJson(jsonString);
        WeaponEffects = ReturnEquipmentSpecificEffects("Weapon");
        ArmorEffects = ReturnEquipmentSpecificEffects("Armor");
    }

    public Effects CreateFromJson(string jsonString){
        return JsonUtility.FromJson<Effects>(jsonString);
    }

    public List<Effect> ReturnEquipmentSpecificEffects(string EquipmentType){
        List<Effect> specificEffects = new List<Effect>();
        foreach(Effect effect in Effects.EffectList){
            if(effect.EquipmentType == EquipmentType){
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
            default:
            return null;
        }
    }

    #region CheckingMethods

    public Effect CheckForWeaponEffects(Effect effect){
        if(effect.LowerPowerBonus != 0){
            effect.PowerBonus = SetRandomValues(effect.LowerPowerBonus, effect.UpperPowerBonus);
        }
        if(effect.LowerLuckBonus != 0){
            effect.LuckBonus = SetRandomValues(effect.LowerLuckBonus, effect.UpperLuckBonus);
        }
        return effect;
    }
    public Effect CheckForArmorEffects(Effect effect){
        if(effect.LowerDefenseBonus != 0){
            effect.DefenseBonus = SetRandomValues(effect.LowerDefenseBonus, effect.UpperDefenseBonus);
            print("Defense bonus for this effect is: " + effect.DefenseBonus);
        }
        if(effect.LowerHealthBonus != 0){
            effect.HealthBonus = SetRandomValues(effect.LowerHealthBonus, effect.UpperHealthBonus);
        }
        if(effect.LowerShieldBonus != 0){
            effect.ShieldBonus = SetRandomValues(effect.LowerShieldBonus, effect.UpperShieldBonus);
        }
        if(effect.LowerLuckBonus != 0){
            effect.LuckBonus = SetRandomValues(effect.LowerLuckBonus, effect.UpperLuckBonus);
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
