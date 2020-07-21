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
    public List<Upgrade> WeaponUpgrades = new List<Upgrade>();
    public List<Upgrade> ArmorUpgrades = new List<Upgrade>();
    public List<Upgrade> AccessoryUpgrades = new List<Upgrade>();
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
            var bullet = other.transform.GetComponent<Bullet>();
            CurrentHealth -= Mathf.RoundToInt(bullet.TotalDamage - Defense);
            bullet.StartCoroutine("Hit");
            // other.transform.gameObject.SetActive(false);
        }
        if(other.tag == "Enemy"){
            CurrentHealth -= Mathf.RoundToInt(other.transform.GetComponent<Actor>().Power - Defense);
        }  
    }
    public void AddStatsFromEquipment(EquipmentType et){
        switch(et){
            case EquipmentType.Weapon:
                //Weapons aren't a modification of the characters stats, but a modification of their attack's stats.
                foreach(Upgrade upgrade in Weapons[CurrentWeapon].Upgrades){
                    Weapons[CurrentWeapon].Attack.BaseDamage += upgrade.BaseDamage;
                    Weapons[CurrentWeapon].Attack.Shots += upgrade.Shots;
                    Weapons[CurrentWeapon].Attack.Size += upgrade.Size;
                    // Weapons[CurrentWeapon].Attack.Accuracy += upgrade.Accuracy;
                    Weapons[CurrentWeapon].Attack.AttackSpeed += upgrade.AttackSpeed;
                    // Weapons[CurrentWeapon].Attack.Distance += upgrade.Distance;
                    // Weapons[CurrentWeapon].Attack.ColdDamage += upgrade.ColdDamage;
                    // Weapons[CurrentWeapon].Attack.FireDamage += upgrade.FireDamage;
                    // Weapons[CurrentWeapon].Attack.ElectricDamage += upgrade.ElectricDamage;
                    // Weapons[CurrentWeapon].Attack.PoisonDamage += upgrade.PoisonDamage;
                    // Weapons[CurrentWeapon].Attack.AcidDamage += upgrade.AcidDamage;
                }
            break;
            case EquipmentType.Armor:
            TotalHealth += Armors[CurrentArmor].HealthBonus;
            Defense += Armors[CurrentArmor].DefenseBonus;
            TotalShield += Armors[CurrentArmor].ShieldBonus;
            Speed += Armors[CurrentArmor].Speed;
            JumpNumber += Armors[CurrentArmor].JumpNumber;
            foreach(Upgrade upgrade in Armors[CurrentArmor].Upgrades){
                TotalHealth += upgrade.HealthBonus;
                Defense += upgrade.DefenseBonus;
                TotalShield += upgrade.ShieldBonus;
                Speed += upgrade.Speed;
                // JumpNumber += upgrade.JumpNumber;
            }
            break;
            case EquipmentType.Accessory:
            TotalHealth += Accessories[CurrentAccessory].HealthBonus;
            Defense += Accessories[CurrentAccessory].DefenseBonus;
            TotalShield += Accessories[CurrentAccessory].ShieldBonus;
            Speed += Accessories[CurrentAccessory].Speed;
            JumpNumber += Accessories[CurrentAccessory].JumpNumber;
            foreach(Upgrade upgrade in Accessories[CurrentAccessory].Upgrades){
                TotalHealth += upgrade.HealthBonus;
                Defense += upgrade.DefenseBonus;
                TotalShield += upgrade.ShieldBonus;
                Speed += upgrade.Speed;
                // JumpNumber += upgrade.JumpNumber;
            }
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
            foreach(Upgrade upgrade in Weapons[CurrentWeapon].Upgrades){
                Weapons[CurrentWeapon].Attack.BaseDamage -= upgrade.BaseDamage;
                Weapons[CurrentWeapon].Attack.Shots -= upgrade.Shots;
                Weapons[CurrentWeapon].Attack.Size -= upgrade.Size;
                // Weapons[CurrentWeapon].Attack.Accuracy += upgrade.Accuracy;
                Weapons[CurrentWeapon].Attack.AttackSpeed -= upgrade.AttackSpeed;
                // Weapons[CurrentWeapon].Attack.Distance += upgrade.Distance;
                // Weapons[CurrentWeapon].Attack.ColdDamage += upgrade.ColdDamage;
                // Weapons[CurrentWeapon].Attack.FireDamage += upgrade.FireDamage;
                // Weapons[CurrentWeapon].Attack.ElectricDamage += upgrade.ElectricDamage;
                // Weapons[CurrentWeapon].Attack.PoisonDamage += upgrade.PoisonDamage;
                // Weapons[CurrentWeapon].Attack.AcidDamage += upgrade.AcidDamage;
            }
            break;
            case EquipmentType.Armor:
            TotalHealth -= Armors[CurrentArmor].HealthBonus;
            Defense -= Armors[CurrentArmor].DefenseBonus;
            TotalShield -= Armors[CurrentArmor].ShieldBonus;
            Speed -= Armors[CurrentArmor].Speed;
            JumpNumber -= Armors[CurrentArmor].JumpNumber;
            foreach(Upgrade upgrade in Armors[CurrentArmor].Upgrades){
                TotalHealth -= upgrade.HealthBonus;
                Defense -= upgrade.DefenseBonus;
                TotalShield -= upgrade.ShieldBonus;
                Speed -= upgrade.Speed;
                // JumpNumber += upgrade.JumpNumber;
            }
            break;
            case EquipmentType.Accessory:
            TotalHealth -= Accessories[CurrentAccessory].HealthBonus;
            Defense -= Accessories[CurrentAccessory].DefenseBonus;
            TotalShield -= Accessories[CurrentAccessory].ShieldBonus;
            Speed -= Accessories[CurrentAccessory].Speed;
            JumpNumber -= Accessories[CurrentAccessory].JumpNumber;
            foreach(Upgrade upgrade in Accessories[CurrentAccessory].Upgrades){
                TotalHealth -= upgrade.HealthBonus;
                Defense -= upgrade.DefenseBonus;
                TotalShield -= upgrade.ShieldBonus;
                Speed -= upgrade.Speed;
                // JumpNumber += upgrade.JumpNumber;
            }
            // Weapon.Attack.BaseDamage -= Accessory.BaseDamage;
            // Weapon.Attack.Shots -= Accessory.Shots;
            // Weapon.Attack.Size -= Accessory.Size;
            // Weapon.Attack.AttackSpeed -= Accessory.AttackSpeed;
            // Weapon.Attack.Accuracy -= Accessory.AccuracyBonus;
            break;
        }

    }
}
