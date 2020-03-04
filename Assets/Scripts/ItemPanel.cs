using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Enums;

public class ItemPanel : MonoBehaviour, ISelectHandler
{
    public Player Player;
    public Menu Menu;
    public int Index;
    public EquipmentType ET;
    public Button Button;

    void Start()
    {
        Menu = GetComponentInParent<Menu>();
        Player = Menu.Player;
        Button = GetComponent<Button>();
    }
    
    public void EquipItem(){
        switch(ET){
            case EquipmentType.Weapon:
            Player.Weapon = Player.Inventory[Index] as Weapon;
            break;
            case EquipmentType.Armor:
            Player.Armor = Player.Inventory[Index] as Armor;
            break;
            // case EquipmentType.Accessory:
            // Player.Accessory = Player.Inventory[itemIndex] as Accessory;
            // break;
            default:
            break;
        }
        Player.RemoveStatsFromEquipment(ET);
        Player.AddStatsFromEquipment(ET);
        
    }
    public void OnSelect(BaseEventData eventData)
    {
        Menu.DetailName.text = Player.Inventory[Index].Name;
        Menu.DetailDescription.text = Player.Inventory[Index].Description;
    }
}
