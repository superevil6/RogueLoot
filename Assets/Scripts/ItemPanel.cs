using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Enums;

public class ItemPanel : MenuPanel//, ISelectHandler, IDeselectHandler
{
    public Player Player;
    public Menu Menu;
    public Image Image;
    public EquipmentType ET;
    public Button Button;
    public UpgradePanel UpgradePanel;
    void Start()
    {
        // Selected = false;
        Menu = GetComponentInParent<Menu>();
        Player = Menu.Player;
        // Button = GetComponent<Button>();
        // UpgradePanel = Menu.GetComponentInChildren<UpgradePanel>();
        Image.color = Color.white;
    }
    
    public void EquipItem(){
        Player.RemoveStatsFromEquipment(ET);
        switch(ET){
            case EquipmentType.Weapon:
            Player.Weapons[Player.CurrentWeapon] = Player.Inventory[Index] as Weapon;
            break;
            case EquipmentType.Armor:
            Player.Armor = Player.Inventory[Index] as Armor;
            break;
            case EquipmentType.Accessory:
            Player.Accessory = Player.Inventory[Index] as Accessory;
            break;
            default:
            break;
        }
        Player.AddStatsFromEquipment(ET);
        // Menu.DetailName.text = Player.Inventory[Index].Name;
        // Menu.DetailDescription.text = Player.Inventory[Index].Description;
        // Menu.ShowUpgradeSlots(Player.Inventory[Index]);
    }

    void Update()
    {
        if(Selected){
            Image.color = Color.red;
            if(Input.GetButtonDown("Confirm")){
                EquipItem();
            }
            if(Input.GetButtonDown("Square")){
                Selected = false;
                Menu.SelectInitialPanel(Menu.EquippedUpgrades.transform, "Equip");
            }
        }
        else{
            Image.color = Color.white;
        }
    }

    // public void OnSelect(BaseEventData eventData)
    // {
    //     Menu.DetailName.text = Player.Inventory[Index].Name;
    //     Menu.DetailDescription.text = Player.Inventory[Index].Description;
    //     Menu.SelectedItem = Index;
    //     Menu.ShowUpgradeSlots(Player.Inventory[Index]);
    // }
    private void OnDisable()
    {
        Selected = false;
        Image.color = Color.white;
    }

}
