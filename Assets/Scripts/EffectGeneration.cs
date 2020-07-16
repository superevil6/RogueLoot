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
            effect = CheckForEffect(WeaponEffects[effectIndex]);
            return effect;
            case EquipmentType.Armor:
            effectIndex = Random.Range(0, ArmorEffects.Count);
            effect = CheckForEffect(ArmorEffects[effectIndex]);
            return effect;
            case EquipmentType.Accessory:
            effectIndex = Random.Range(0, AccessoryEffects.Count);
            effect = CheckForEffect(AccessoryEffects[effectIndex]);
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
            newEffect = CheckForEffect(WeaponEffects[effectIndex]);
            return newEffect;
            case EquipmentType.Armor:
            effectIndex = Random.Range(0, ArmorEffects.Count);
            newEffect = CheckForEffect(ArmorEffects[effectIndex]);
            return newEffect;
            case EquipmentType.Accessory:
            effectIndex = Random.Range(0, AccessoryEffects.Count);
            newEffect = CheckForEffect(AccessoryEffects[effectIndex]);
            return newEffect;
            case EquipmentType.Upgrade:
            effectIndex = Random.Range(0, AccessoryEffects.Count);
            newEffect = CheckForEffect(AccessoryEffects[effectIndex]);
            return newEffect;
            default:
            return null;
        }
    }

    #region CheckingMethods

    public Effect CheckForEffect(Effect effect){
        if(effect.Lower != 0 && effect.Upper != 0 && effect.Bonus == 0){
            effect.Bonus = SetRandomValues(effect.Lower, effect.Upper);
        }
        effect.Description = effect.Type + " " + effect.Bonus + "\n";
        return effect;
    }
    #endregion

    #region Utility Methods
    public float SetRandomValues(float valueOne, float valueTwo){
        return Random.Range(valueOne, valueTwo);
    }

    #endregion
}
