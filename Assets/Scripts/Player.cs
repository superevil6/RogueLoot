﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.UI;

public class Player : Actor
{
    public Weapon Weapon;
    public int CurrentWeapon = 0;
    public List<Weapon> Weapons = new List<Weapon>();
    public Armor Armor;
    public Accessory Accessory;
    //public List<Accessory> Accessories = new List<Accessory>();
    public List<Item> Inventory = new List<Item>();
    public Sprite Sprite;
    public int Luck;
    public Text EquippedWeapon;
    public Text ModuleText;
    public Image HealthBar;


    void Start()
    {
        Bullets.InstantiateObjects(NumberOfPooledBullets);
        AddStatsFromEquipment(EquipmentType.Weapon);
        AddStatsFromEquipment(EquipmentType.Armor);
        AddStatsFromEquipment(EquipmentType.Accessory);

    }

    // Update is called once per frame
    void Update()
    {
        EquippedWeapon.text = Weapons[CurrentWeapon].Name;
        if(Weapons[CurrentWeapon].Upgrades.Count >= 1){
            ModuleText.text = Weapons[CurrentWeapon].Upgrades[0].Name;
        }
        HealthBar.fillAmount = (float)CurrentHealth / (float)TotalHealth;
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "EnemyBullet"){
            CurrentHealth -= Mathf.RoundToInt(other.transform.GetComponent<Bullet>().TotalDamage - Defense);
            other.transform.gameObject.SetActive(false);
            print(CurrentHealth);
        }
        if(other.tag == "Enemy"){
            CurrentHealth -= Mathf.RoundToInt(other.transform.GetComponent<Actor>().Power - Defense);
        }  
    }
    public void AddStatsFromEquipment(EquipmentType et){
        switch(et){
            case EquipmentType.Weapon:
                
            break;
            case EquipmentType.Armor:
            // TotalHealth += Armor.HealthBonus;
            // Defense += Armor.DefenseBonus;
            // TotalShield += Armor.ShieldBonus;
            // Speed += Armor.Speed;
            break;
            case EquipmentType.Accessory:
            TotalHealth += Accessory.HealthBonus;
            Defense += Accessory.DefenseBonus;
            TotalShield += Accessory.ShieldBonus;
            Speed += Accessory.Speed;
            Weapon.Attack.BaseDamage += Accessory.BaseDamage;
            Weapon.Attack.Shots += Accessory.Shots;
            Weapon.Attack.Size += Accessory.Size;
            Weapon.Attack.AttackSpeed += Accessory.AttackSpeed;
            break;
        }

    }
    public void RemoveStatsFromEquipment(EquipmentType et){
        switch(et){
            case EquipmentType.Weapon:

            break;
            case EquipmentType.Armor:
            TotalHealth -= Armor.HealthBonus;
            Defense -= Armor.DefenseBonus;
            TotalShield -= Armor.ShieldBonus;
            Speed -= Armor.Speed;
            break;
            case EquipmentType.Accessory:
            TotalHealth -= Accessory.HealthBonus;
            Defense -= Accessory.DefenseBonus;
            TotalShield -= Accessory.ShieldBonus;
            Speed -= Accessory.Speed;
            Weapon.Attack.BaseDamage -= Accessory.BaseDamage;
            Weapon.Attack.Shots -= Accessory.Shots;
            Weapon.Attack.Size -= Accessory.Size;
            Weapon.Attack.AttackSpeed -= Accessory.AttackSpeed;
            Weapon.Attack.Accuracy -= Accessory.AccuracyBonus;
            break;
        }

    }
}
