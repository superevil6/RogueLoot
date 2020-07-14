using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Enums;

public class UpgradePanel : MenuPanel
{
    public Player Player;
    public Menu Menu;
    public int SlotIndex;
    public int UpgradeIndex;
    public EquipmentType ET;
    public Text Name;
    public Text Description;
    public Image Image;

    // Start is called before the first frame update
    void Start()
    {
        Menu = GetComponentInParent<Menu>();
        Player = Menu.Player;
        // Player = Menu.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if(Selected){
            Menu.InMainMenu = false;
            Image.color = Color.red;
            if(PanelType == "Equipped"){
                Menu.InEquippedUpgradesMenu = true;
                if(Input.GetButtonDown("Confirm") && Menu.CanMoveCursor){
                    Menu.InEquipableUpgradesMenu = true;
                    Menu.InEquippedUpgradesMenu = false;
                    print("on Equipped");

                    Menu.SelectInitialPanel(Menu.UnequippedUpgrades.transform, "Unequip");
                    Selected = false;
                    StartCoroutine(Menu.WaitBetweenInputs(Menu.WaitTime));
                }
                // if(Input.GetButtonDown("Square") && Menu.CanMoveCursor){
                //     UnequipUpgrade();
                //     StartCoroutine(Menu.WaitBetweenInputs(Menu.WaitTime));
                // }
                if(Input.GetButtonDown("Cancel") && Menu.CanMoveCursor){
                    Menu.InMainMenu = true;
                    Menu.InEquipableUpgradesMenu = false;
                    Selected = false;
                    Menu.SelectPanel(Menu.InventoryPanel.transform, Menu.CurrentlySelectedItemPanel, "ItemPanel");
                    StartCoroutine(Menu.WaitBetweenInputs(Menu.WaitTime));
                }
            }
            if(PanelType == "ToEquip"){ 
                Menu.InEquipableUpgradesMenu = true;
                if(Input.GetButtonDown("Confirm") && Menu.CanMoveCursor){
                    Menu.InEquippedUpgradesMenu = true;
                    Menu.InEquipableUpgradesMenu = false;
                    print("What the fuck!?");
                    // Menu.SelectInitialPanel(Menu.EquippedUpgrades.transform, "Equipped");
                    if(Player.Inventory[Menu.CurrentlySelectedItemPanel].Upgrades.ElementAtOrDefault(SlotIndex) != null){
                        print("Whaaaa?!");
                        UnequipUpgrade(SlotIndex);
                    }
                    // UnequipUpgrade(AlreadyEquipedUpgradeIndex);
                    EquipUpgrade(UpgradeIndex);
                    StartCoroutine(Menu.WaitBetweenInputs(Menu.WaitTime));
                }
                if(Input.GetButtonDown("Cancel") && Menu.CanMoveCursor){
                    Selected = false;
                    Menu.InMainMenu = true;
                    Menu.InEquipableUpgradesMenu = false;
                    Menu.SelectPanel(Menu.EquippedUpgrades.transform, 0, "Equipped");
                    StartCoroutine(Menu.WaitBetweenInputs(Menu.WaitTime));
                }
 
            }
            if(PanelType == "Unequip"){ 
                if(Input.GetButtonDown("Confirm") && Menu.CanMoveCursor){
                    UnequipUpgrade(UpgradeIndex);
                    Menu.SelectPanel(Menu.EquippedUpgrades.transform, 0, "Equipped");
                    StartCoroutine(Menu.WaitBetweenInputs(Menu.WaitTime));
                }
                if(Input.GetButtonDown("Cancel") && Menu.CanMoveCursor){
                    Selected = false;
                    Menu.SelectPanel(Menu.EquippedUpgrades.transform, 0, "Equipped");
                    StartCoroutine(Menu.WaitBetweenInputs(Menu.WaitTime));
                }
            }

        }
        else{
            Image.color = Color.white;
        }
    }
    public void EquipUpgrade(int index){
        if(Player.Inventory[Menu.CurrentlySelectedItemPanel].Upgrades.Count < Player.Inventory[Menu.CurrentlySelectedItemPanel].UpgradeSlots){
            print(transform.parent.transform.GetChild(SlotIndex).GetComponent<MenuPanel>().PanelType);
            Player.Inventory[Menu.CurrentlySelectedItemPanel].Upgrades.Add(Player.Inventory[index]);
            Player.Inventory[UpgradeIndex].Equipped = true;
            Player.RemoveStatsFromEquipment(ET);
            Player.AddStatsFromEquipment(ET);
            Menu.ShowAvailableUpgrades(ET);
            Menu.ShowUpgradeSlots(Player.Inventory[Menu.CurrentlySelectedItemPanel]);
        }
    }
    public void UnequipUpgrade(int index){
        Player.Inventory[Menu.CurrentlySelectedItemPanel].Upgrades[index].Equipped = false;
        Player.Inventory[Menu.CurrentlySelectedItemPanel].Upgrades.RemoveAt(index);
        Player.RemoveStatsFromEquipment(ET);
        Player.AddStatsFromEquipment(ET);
        Menu.ShowUpgradeSlots(Player.Inventory[Menu.CurrentlySelectedItemPanel]);
        Menu.ShowAvailableUpgrades(ET);

    }
}
