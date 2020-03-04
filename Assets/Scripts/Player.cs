using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.UI;

public class Player : Actor
{
    public Weapon Weapon;
    public Armor Armor;
    public List<Accessory> Accessories = new List<Accessory>();
    public List<Item> Inventory = new List<Item>();
    public Sprite Sprite;
    public int Luck;


    void Start()
    {
        Bullets.InstantiateObjects(NumberOfPooledBullets);
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                TotalHealth += Armor.HealthBonus;
                Defense += Armor.DefenseBonus;
                TotalShield += Armor.ShieldBonus;
                Speed += Armor.Speed;
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
        }

    }
}
