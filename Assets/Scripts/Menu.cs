using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Enums;

public class Menu : MonoBehaviour
{
    public Player Player;
    public GameObject MenuPanel;
    // public GameObject Cursor;
    public GameObject InventoryPanel;
    public GameObject ItemPanel;
    public GameObject EquippedUpgrades;
    public GameObject UnequippedUpgrades;
    public GameObject EquipUpgradeButton;
    public Image DetailImage;
    public Text DetailName;
    public Text DetailDescription;
    public EventSystem ES;
    private bool MenuOpen;
    public EquipmentType[] MenuOrder = new EquipmentType[]{EquipmentType.Weapon, EquipmentType.Armor, EquipmentType.Accessory};
    public int CurrentlySelectedItemPanel;
    public int CurrentlySelectedEquippedUpgradePanel;
    public int CurrentlySelectedUnequippedUpgradePanel;
    public int MenuIndex;
    public bool InMainMenu;
    public bool InEquippedUpgradesMenu;
    public bool InEquipableUpgradesMenu; 
    public bool CanMoveCursor;
    public float WaitTime;

    // Start is called before the first frame update
    void Awake()
    {
        MenuIndex = 0;
        WaitTime = 0.15f;
        MakeInventory(Player.Inventory);

    }
    void OnEnable()
    {
        CanMoveCursor = true;
        InMainMenu = true;
        InEquippedUpgradesMenu = false;
        InEquipableUpgradesMenu = false;
        ShowInventory(MenuOrder[MenuIndex]);
        ShowAvailableUpgrades(MenuOrder[MenuIndex]);
        // SelectInitialPanel(InventoryPanel.transform, "ItemPanel");
    }

    void Update()
    {
        if(CanMoveCursor){
            if(Input.GetButtonDown("L button")){
                CycleMenuIndex(false);
                HideInventory();
                ShowInventory(MenuOrder[MenuIndex]);
                ShowAvailableUpgrades(MenuOrder[MenuIndex]);
                SelectInitialPanel(InventoryPanel.transform, "ItemPanel");
            }
            if(Input.GetButtonDown("R button")){
                CycleMenuIndex(true);
                HideInventory();
                ShowInventory(MenuOrder[MenuIndex]);
                ShowAvailableUpgrades(MenuOrder[MenuIndex]);
                SelectInitialPanel(InventoryPanel.transform, "ItemPanel");
                StartCoroutine(WaitBetweenInputs(WaitTime));
            }
            if(Input.GetAxisRaw("VerticalLeft") == -1){
                if(InMainMenu){
                    SelectPanel(InventoryPanel.transform, CurrentlySelectedItemPanel, "up", "ItemPanel");
                }
                if(InEquipableUpgradesMenu){
                    SelectPanel(UnequippedUpgrades.transform, CurrentlySelectedUnequippedUpgradePanel, "up", "ToEquip");
                }
            }
            if(Input.GetAxisRaw("VerticalLeft") == 1){
                if(InMainMenu){
                    SelectPanel(InventoryPanel.transform, CurrentlySelectedItemPanel, "down", "ItemPanel");
                }
                if(InEquipableUpgradesMenu){
                    SelectPanel(UnequippedUpgrades.transform, CurrentlySelectedUnequippedUpgradePanel, "down", "ToEquip");
                }
            }
            if(Input.GetAxisRaw("HorizontalLeft") == 1){
                if(InEquippedUpgradesMenu){
                    SelectPanel(EquippedUpgrades.transform, CurrentlySelectedEquippedUpgradePanel, "left", "Equipped");
                }
            }
            if(Input.GetAxisRaw("HorizontalLeft") == -1){
                if(InEquippedUpgradesMenu){
                    SelectPanel(EquippedUpgrades.transform, CurrentlySelectedEquippedUpgradePanel, "right", "Equipped");
                }
            }
        }

    }

   
    #region Item and Upgrade showing/hiding
    public void CycleMenuIndex(bool right){
        if(right){
            if(MenuIndex < MenuOrder.Length -1){
                MenuIndex += 1;
            }
            else{
                MenuIndex = 0;
            }
        }
        else{
            if(MenuIndex > 0){
                MenuIndex -= 1;
            }
            else{
                MenuIndex = MenuOrder.Length -1;
            }
        }
    }
    public void MakeInventory(List<Item> Inventory){
        for(int i = 0; i < Inventory.Count; i++){
            GameObject go = Instantiate(ItemPanel);
            go.transform.SetParent(InventoryPanel.transform);
            go.transform.localScale = go.transform.parent.localScale;
            go.GetComponent<ItemPanel>().Index = i;
            go.GetComponent<ItemPanel>().ET = Inventory[i].EquipmentType;
            go.GetComponent<ItemPanel>().Player = Player;
            go.SetActive(false);
            var text = go.GetComponentsInChildren<Text>();
            text[0].text = Inventory[i].Name;
            foreach(Effect effect in Inventory[i].Effects){
                text[1].text += effect.Name;
            }
        }
    }
    public void ShowInventory(EquipmentType subMenu){
        foreach(Transform child in InventoryPanel.transform){
            if(child.GetComponent<ItemPanel>().ET == subMenu){
                child.gameObject.SetActive(true);
            }
        }
        SelectInitialPanel(InventoryPanel.transform, "ItemPanel");
    }
    public void HideInventory(){
        foreach(Transform child in InventoryPanel.transform){
            child.gameObject.SetActive(false);
        }
    }
    public void ShowAvailableUpgrades(EquipmentType ET){
        DestroyAvailableUpgrades();
        GameObject UnequipButton = Instantiate(EquipUpgradeButton);
        UnequipButton.GetComponent<UpgradePanel>().Name.text = "Unequip Upgrade";
        UnequipButton.GetComponent<UpgradePanel>().PanelType = "Unequip";
        UnequipButton.transform.SetParent(UnequippedUpgrades.transform);
        UnequipButton.transform.localPosition = UnequipButton.transform.parent.transform.localPosition;
        UnequipButton.transform.localScale = UnequipButton.transform.parent.transform.localScale;
        for(int i = 0; i < Player.Inventory.Count; i++){
            if(Player.Inventory[i].EquipmentType == EquipmentType.Upgrade && Player.Inventory[i].UpgradeType == ET && Player.Inventory[i].Equipped == false)
            {
                GameObject go = Instantiate(EquipUpgradeButton);
                go.GetComponent<UpgradePanel>().PanelType = "ToEquip";
                go.GetComponent<UpgradePanel>().UpgradeIndex = i;
                go.GetComponent<UpgradePanel>().ET = Player.Inventory[i].EquipmentType;
                go.transform.SetParent(UnequippedUpgrades.transform);
                go.transform.localScale = go.transform.parent.transform.localScale;
                go.transform.localPosition = go.transform.parent.transform.localPosition;
                go.GetComponent<UpgradePanel>().Name.text = Player.Inventory[i].Name;
                go.GetComponent<UpgradePanel>().Description.text = Player.Inventory[i].Description;
            }
        }
    }
    public void ShowUpgradeSlots(Item item){
        DestroyUpgradeSlots();
        if(item.UpgradeSlots > 0){
            for(int i = 0; i < item.UpgradeSlots; i++){
                GameObject go = Instantiate(EquipUpgradeButton);
                go.transform.SetParent(EquippedUpgrades.transform);
                go.transform.localScale = go.transform.parent.localScale;
                go.GetComponent<UpgradePanel>().PanelType = "Equipped";
                go.GetComponent<UpgradePanel>().SlotIndex = i;
                go.GetComponent<UpgradePanel>().ET = item.EquipmentType;
                if(item.Upgrades.Count > 0){
                    go.GetComponent<UpgradePanel>().Name.text = item.Upgrades[i].Name;
                }
                else{
                    go.GetComponent<UpgradePanel>().Name.text = "Empty";
                }
            }
        }
    }
    public void DestroyUpgradeSlots(){
        foreach(Transform child in EquippedUpgrades.transform){
            GameObject.Destroy(child.gameObject);
        }
    }
    public void DestroyAvailableUpgrades(){
        foreach(Transform child in UnequippedUpgrades.transform){
            GameObject.Destroy(child.gameObject);
        }
    }
    #endregion
   
    #region SelectionMethods
    public void SelectPanel(Transform transform, int currentPanelIndex, string direction, string PanelType){
        if(direction == "up" || direction == "left"){
            for(int i = currentPanelIndex +1; i < transform.childCount; i++){
                if(transform.GetChild(i).gameObject.activeInHierarchy){
                    DeselectAllPanels(transform);
                    transform.GetChild(i).GetComponent<MenuPanel>().Selected = true;
                    if(PanelType == "ItemPanel"){
                        DetailName.text = Player.Inventory[i].Name;
                        DetailDescription.text = Player.Inventory[i].Description;
                        CurrentlySelectedItemPanel = i;
                        ShowUpgradeSlots(Player.Inventory[i]);

                    }
                    if(PanelType == "ToEquip"){
                        CurrentlySelectedUnequippedUpgradePanel = i;
                    }
                    if(PanelType == "Equipped"){
                        CurrentlySelectedEquippedUpgradePanel = i;
                    }
                    break;
                }
            }
        }
        else{
            for(int i = currentPanelIndex -1; i >= 0; i--){
                if(transform.GetChild(i).gameObject.activeInHierarchy){
                    DeselectAllPanels(transform);
                    transform.GetChild(i).GetComponent<MenuPanel>().Selected = true;
                    if(PanelType == "ItemPanel"){
                        DetailName.text = Player.Inventory[i].Name;
                        DetailDescription.text = Player.Inventory[i].Description;
                        CurrentlySelectedItemPanel = i;
                        ShowUpgradeSlots(Player.Inventory[i]);
                    }
                    if(PanelType == "ToEquip"){
                        CurrentlySelectedUnequippedUpgradePanel = i;
                    }
                    if(PanelType == "Equipped"){
                        CurrentlySelectedEquippedUpgradePanel = i;
                    }
                    break;
                }
            }
        }
        StartCoroutine(WaitBetweenInputs(WaitTime));
    }
    //This one picks a specific panel
    public void SelectPanel(Transform transform, int currentPanelIndex, string PanelType){
        //We can assume this panel is active, becuase by using this method we're essentially doing a back action.
        transform.GetChild(currentPanelIndex).GetComponent<MenuPanel>().Selected = true;
        StartCoroutine(WaitBetweenInputs(WaitTime));

    }
    public void SelectInitialPanel(Transform transform, string PanelType){
        DeselectAllPanels(transform);
        for(int i = 0; i < transform.childCount; i++){
            if(transform.GetChild(i).gameObject.activeInHierarchy){
                transform.GetChild(i).gameObject.GetComponent<MenuPanel>().Selected = true;
                if(PanelType == "ItemPanel"){
                    CurrentlySelectedItemPanel = i;
                    DetailName.text = Player.Inventory[i].Name;
                    DetailDescription.text = Player.Inventory[i].Description;
                    ShowUpgradeSlots(Player.Inventory[i]);

                }
                if(PanelType == "ToEquip" || PanelType == "Unequip"){
                    CurrentlySelectedUnequippedUpgradePanel = i;
                }
                break;
            }
        }
        StartCoroutine(WaitBetweenInputs(WaitTime));

    }
    public void DeselectAllPanels(Transform transform){
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<MenuPanel>().Selected = false;
        }
    }
    #endregion
    public IEnumerator WaitBetweenInputs(float waitTime){
        CanMoveCursor = false;
        yield return new WaitForSeconds(waitTime);
        CanMoveCursor = true;
    }
}
