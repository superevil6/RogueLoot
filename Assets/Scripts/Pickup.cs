using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class Pickup : MonoBehaviour
{
    public PickupType PickupType;
    public BoxCollider2D BC;
    public Rigidbody2D RB;
    public EquipmentGeneration EG;
    public SpriteRenderer SR;
    public Item Item;
    public GameObject ItemPanel;
    // Start is called before the first frame update
    void Start()
    {
        EG = GetComponentInParent<EquipmentGeneration>();
        PickupType = SelectPickupType();
        if(PickupType == PickupType.Upgrade || 
        PickupType == PickupType.Weapon ||
        PickupType == PickupType.Armor ||
        PickupType == PickupType.Accessory){
            Item = GenerateItem(PickupType);
            SR.color = GetRarityColor(Item.Rarity);
        }
        if(PickupType == PickupType.Money){
            SR.color = Color.green;
            //Generate Money
        }
        if(PickupType == PickupType.Health){
            SR.color = Color.red;
            //Generate Health
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player"){
            var player = other.gameObject.GetComponent<Player>();
            if(PickupType == PickupType.Upgrade || 
            PickupType == PickupType.Weapon ||
            PickupType == PickupType.Armor ||
            PickupType == PickupType.Accessory){
                ItemPanel.SetActive(true);
                if(Input.GetButtonDown("Confirm")){
                    // if(PickupType == PickupType.Upgrade || 
                    // PickupType == PickupType.Weapon ||
                    // PickupType == PickupType.Armor ||
                    // PickupType == PickupType.Accessory){
                    //     other.gameObject.GetComponent<Player>().Inventory.Add(Item);
                    // }
                    if(PickupType == PickupType.Weapon){
                        player.Weapons.Add((Weapon)Item);
                    }
                    if(PickupType == PickupType.Upgrade){
                        if(Item.UpgradeType == EquipmentType.Weapon){
                            player.Weapons[player.CurrentWeapon].Upgrades.Add(Item);
                        }
                    }
                    gameObject.SetActive(false);
                }
            }
            if(PickupType == PickupType.Money){
                //Consider randomizing a value that has different sprites for quantity
                other.gameObject.GetComponent<Player>().Money += 10;
                gameObject.SetActive(false);
            }
            if(PickupType == PickupType.Health){
                if(player.CurrentHealth < player.TotalHealth){
                    player.CurrentHealth += 10;
                }
                if(player.CurrentHealth + 10 > player.TotalHealth){
                    player.CurrentHealth = player.TotalHealth;
                }
                gameObject.SetActive(false);
            }

        }
    }
    void OnTriggerExit2D(Collider2D other) {
        ItemPanel.SetActive(false);
    }
    public PickupType SelectPickupType(){
        int typeRNG = Random.Range(0, 100);
        if(typeRNG <= 10){
            typeRNG = Random.Range(1, 4);
            switch(typeRNG){
                case 1: 
                return PickupType.Weapon;
                case 2:
                return PickupType.Armor;
                case 3:
                return PickupType.Accessory;
            }
        }
        if(typeRNG > 10 && typeRNG < 30){
            return PickupType.Upgrade;
        }
        else{
            typeRNG = Random.Range(1, 3);
            switch(typeRNG){
                case 1:
                return PickupType.Money;
                case 2:
                return PickupType.Health;
            }
        }
        return PickupType.Money;
    }
    public Item GenerateItem(PickupType type){
        switch(type){
            case PickupType.Weapon:
            return EG.GenerateWeapon();
            case PickupType.Armor:
            return EG.GenerateArmor();
            case PickupType.Accessory:
            return EG.GenerateAccessory();
            case PickupType.Upgrade:
            return EG.GenerateUpgrade();
            // case PickupType.Money:
            // return 
        }
        return null;
    }
    public Color GetRarityColor(Rarity rarity){
        switch(rarity){
            case Rarity.Legendary:
            return new Color32(255, 128, 0, 255);
            case Rarity.Rare:
            return Color.yellow;
            case Rarity.Magical:
            return Color.blue;
            case Rarity.Normal:
            return Color.white;
            default:
            return Color.gray;
        }
    }
}
