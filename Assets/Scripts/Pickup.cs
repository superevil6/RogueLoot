using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Pickup : MonoBehaviour
{
    public PickupType PickupType;
    public BoxCollider2D BC;
    public Rigidbody2D RB;
    public EquipmentGeneration EG;
    public Item Item;
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
        }
        if(PickupType == PickupType.Money){
            //Generate Money
        }
        if(PickupType == PickupType.Health){
            //Generate Health
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(PickupType == PickupType.Upgrade || 
            PickupType == PickupType.Weapon ||
            PickupType == PickupType.Armor ||
            PickupType == PickupType.Accessory){
                other.gameObject.GetComponent<Player>().Inventory.Add(Item);
                print(JsonUtility.ToJson(Item));
            }
            if(PickupType == PickupType.Money){
                //Give Money
            }
            if(PickupType == PickupType.Health){
                //Give Health
            }
            gameObject.SetActive(false);
        }
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
            // case PickupType.Accessory:
            // return EG.GenerateAccessory();
            // case PickupType.Upgrade:
            // return EG.GenerateUpgrade();
            // case PickupType.Money:
            // return 
        }
        return null;
    }
}
