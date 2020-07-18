using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.UI;

public class Player : Actor
{
    public Weapon Weapon;
    public int CurrentWeapon = 0;
    public int CurrentArmor = 0;
    public int CurrentAccessory = 0;
    public List<Weapon> Weapons = new List<Weapon>();
    public List<Armor> Armors = new List<Armor>();
    public List<Accessory> Accessories = new List<Accessory>();
    public List<Item> Inventory = new List<Item>();
    public Sprite Sprite;
    public int Luck;
    public Text EquippedWeapon;
    public Text EquippedArmor;
    public Text ArmorModule;
    public Text ModuleText;
    public Text MoneyText;
    public Image HealthBar;
    public int Money;


    void Start()
    {
        Bullets.InstantiateObjects(NumberOfPooledBullets);

    }

    // Update is called once per frame
    void Update()
    {
        EquippedWeapon.text = Weapons[CurrentWeapon].Name;
        MoneyText.text = Money.ToString();
        EquippedArmor.text = Armors[CurrentArmor].Name;
        if(Weapons[CurrentWeapon].Upgrades.Count >= 1){
            ModuleText.text = Weapons[CurrentWeapon].Upgrades[0].Name;
        }
        HealthBar.fillAmount = (float)CurrentHealth / (float)TotalHealth;
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "EnemyBullet"){
            CurrentHealth -= Mathf.RoundToInt(other.transform.GetComponent<Bullet>().TotalDamage - Defense);
            other.transform.gameObject.SetActive(false);
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
            TotalHealth += Armors[CurrentArmor].HealthBonus;
            Defense += Armors[CurrentArmor].DefenseBonus;
            TotalShield += Armors[CurrentArmor].ShieldBonus;
            Speed += Armors[CurrentArmor].Speed;
            JumpNumber += Armors[CurrentArmor].JumpNumber;
            break;
            case EquipmentType.Accessory:
            TotalHealth += Accessories[CurrentAccessory].HealthBonus;
            Defense += Accessories[CurrentAccessory].DefenseBonus;
            TotalShield += Accessories[CurrentAccessory].ShieldBonus;
            Speed += Accessories[CurrentAccessory].Speed;
            JumpNumber += Accessories[CurrentAccessory].JumpNumber;
            // Weapon.Attack.BaseDamage += Accessory.BaseDamage;
            // Weapon.Attack.Shots += Accessory.Shots;
            // Weapon.Attack.Size += Accessory.Size;
            // Weapon.Attack.AttackSpeed += Accessory.AttackSpeed;
            break;
        }

    }
    public void RemoveStatsFromEquipment(EquipmentType et){
        switch(et){
            case EquipmentType.Weapon:

            break;
            case EquipmentType.Armor:
            TotalHealth -= Armors[CurrentArmor].HealthBonus;
            Defense -= Armors[CurrentArmor].DefenseBonus;
            TotalShield -= Armors[CurrentArmor].ShieldBonus;
            Speed -= Armors[CurrentArmor].Speed;
            break;
            case EquipmentType.Accessory:
            TotalHealth -= Accessories[CurrentAccessory].HealthBonus;
            Defense -= Accessories[CurrentAccessory].DefenseBonus;
            TotalShield -= Accessories[CurrentAccessory].ShieldBonus;
            Speed -= Accessories[CurrentAccessory].Speed;
            JumpNumber -= Accessories[CurrentAccessory].JumpNumber;
            // Weapon.Attack.BaseDamage -= Accessory.BaseDamage;
            // Weapon.Attack.Shots -= Accessory.Shots;
            // Weapon.Attack.Size -= Accessory.Size;
            // Weapon.Attack.AttackSpeed -= Accessory.AttackSpeed;
            // Weapon.Attack.Accuracy -= Accessory.AccuracyBonus;
            break;
        }

    }
}
