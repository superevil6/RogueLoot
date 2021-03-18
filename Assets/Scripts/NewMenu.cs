using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class NewMenu : MonoBehaviour
{
    public Player Player;
    public Controls Controls;
    public bool Active;
    private string[] SubMenus = new string[]{"Weapon", "Armor", "Accessory"};
    private int WeaponIndex = 0;
    private int ArmorIndex = 0;
    private int AccessoryIndex = 0;
    private int WeaponUpgradeIndex = 0;
    private int ArmorUpgradeIndex = 0;
    private int AccessoryUpgradeIndex = 0;
    private int ActiveSubMenu = 0;
    private float InputTimerCooldown = 0;
    private float InputTimerCooldownAmount = 0.15f;
    //UI
    public GameObject MenuHolder;
    public Text ItemName;
    public Image ItemImage;
    public Text ItemStats;
    public GameObject MiniUpgradePanel;
    public GameObject MiniUpgradePanelParent;
    public int MiniUpgradePanelCount;
    public List<GameObject> MiniUpgradePanels = new List<GameObject>();
    public GameObject UpgradePanel;
    private bool InUpgradeMenu;
    public Text UpgradeName;
    public Text UpgradeDesc;
    public Image UpgradeImage;
    public GameObject PickUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ActiveSubMenu = 0;
        InUpgradeMenu = false;
        WeaponIndex = 0;
        ArmorIndex = 0;
        AccessoryIndex = 0;
        GenerateMiniPanels();
        InputTimerCooldown = InputTimerCooldownAmount;
        MenuHolder.transform.position = new Vector2(Player.transform.localPosition.x, Player.transform.localPosition.y + 200);
        SetItemInfo(ActiveSubMenu);
        SetMiniPanelsInfo(ActiveSubMenu);
    }
    void OnEnable() {
        InputTimerCooldown = InputTimerCooldownAmount;
        MenuHolder.transform.position = new Vector2(Player.transform.localPosition.x, Player.transform.localPosition.y + 200);
    }
    // Update is called once per frame
    void Update()
    {
        MenuHolder.transform.localPosition = new Vector2(Player.transform.localPosition.x, Player.transform.localPosition.y + 200);
        if(InputTimerCooldown <= 0){
            if(!InUpgradeMenu){
                if(Input.GetAxis("Menu Horizontal") == -1){
                    switch(ActiveSubMenu){
                        case 0:
                            if(WeaponIndex > 0){
                                WeaponIndex -= 1;
                            }
                            else{
                                WeaponIndex = Player.Weapons.Count - 1;
                            }
                        break;
                        case 1:
                            if(ArmorIndex > 0){
                                ArmorIndex -= 1;
                            }
                            else{
                                ArmorIndex = Player.Armors.Count - 1;
                            }
                        break;

                        case 2:
                            if(AccessoryIndex > 0){
                                AccessoryIndex -= 1;
                            }
                            else{
                                AccessoryIndex = Player.Accessories.Count - 1;
                            }
                        break;
                    }
                    InputTimerCooldown = InputTimerCooldownAmount;
                    SetItemInfo(ActiveSubMenu);
                    SetMiniPanelsInfo(ActiveSubMenu);
                } 
                if(Input.GetAxisRaw("Menu Horizontal") == 1){
                    switch(ActiveSubMenu){
                        case 0:
                            if(WeaponIndex < Player.Weapons.Count - 1){
                                WeaponIndex += 1;
                            }
                            else{
                                WeaponIndex = 0;
                            }
                        break;
                        case 1:
                            if(ArmorIndex < Player.Armors.Count - 1){
                                ArmorIndex += 1;
                            }
                            else{
                                ArmorIndex = 0;
                            }
                        break;

                        case 2:
                            if(AccessoryIndex < Player.Accessories.Count - 1){
                                AccessoryIndex += 1;
                            }
                            else{
                                AccessoryIndex = 0;
                            }
                        break;
                    }
                    InputTimerCooldown = InputTimerCooldownAmount;
                    SetItemInfo(ActiveSubMenu);
                    SetMiniPanelsInfo(ActiveSubMenu);
                    //Next Item
                }
                if(Input.GetAxisRaw("Menu Vertical") == 1){
                    if(ActiveSubMenu < SubMenus.Length - 1){
                        ActiveSubMenu += 1;
                    }
                    else{
                        ActiveSubMenu = 0;
                    }
                    SetItemInfo(ActiveSubMenu);
                    SetMiniPanelsInfo(ActiveSubMenu);
                    InputTimerCooldown = InputTimerCooldownAmount;
                    //Previous Category
                }
                if(Input.GetAxisRaw("Menu Vertical") == -1){
                    if(ActiveSubMenu > 0){
                        ActiveSubMenu -= 1;
                    }
                    else{
                        ActiveSubMenu = SubMenus.Length - 1;
                    }
                    SetItemInfo(ActiveSubMenu);
                    SetMiniPanelsInfo(ActiveSubMenu);
                    InputTimerCooldown = InputTimerCooldownAmount;
                    //Next Category
                }
                if(Input.GetButtonDown("Confirm")){
                    EquipItem(ActiveSubMenu);
                }
                if(Input.GetButtonDown("Square") && CheckAvailableSlot(ActiveSubMenu) && CheckIfPlayerHasUpgrades(ActiveSubMenu)){
                        UpgradePanel.SetActive(true);
                        SetUpgradeInfo(ActiveSubMenu);
                        // SetMiniPanelsInfo(ActiveSubMenu);
                        InUpgradeMenu = true;
                    }
                }
                if(Input.GetButtonDown("Drop")){
                    //Make Pickup spawn
                    DropItem(ActiveSubMenu);
                    //Remove all pickups when droppping
                }
            if(InUpgradeMenu){
                if(Input.GetAxis("Menu Horizontal") == -1){
                    switch(ActiveSubMenu){
                        case 0:
                            if(WeaponUpgradeIndex > 0){
                                WeaponUpgradeIndex -= 1;
                            }
                            else{
                                WeaponUpgradeIndex = Player.WeaponUpgrades.Count - 1;
                            }
                        break;
                        case 1:
                            if(ArmorUpgradeIndex > 0){
                                ArmorUpgradeIndex -= 1;
                            }
                            else{
                                ArmorUpgradeIndex = Player.ArmorUpgrades.Count - 1;
                            }
                        break;

                        case 2:
                            if(AccessoryUpgradeIndex > 0){
                                AccessoryUpgradeIndex -= 1;
                            }
                            else{
                                AccessoryUpgradeIndex = Player.AccessoryUpgrades.Count - 1;
                            }
                        break;
                    }
                    InputTimerCooldown = InputTimerCooldownAmount;
                    SetUpgradeInfo(ActiveSubMenu);
                } 
                if(Input.GetAxisRaw("Menu Horizontal") == 1){
                    switch(ActiveSubMenu){
                        case 0:
                            if(WeaponUpgradeIndex < Player.WeaponUpgrades.Count - 1){
                                WeaponUpgradeIndex += 1;
                            }
                            else{
                                WeaponUpgradeIndex = 0;
                            }
                        break;
                        case 1:
                            if(ArmorUpgradeIndex < Player.ArmorUpgrades.Count - 1){
                                ArmorUpgradeIndex += 1;
                            }
                            else{
                                ArmorUpgradeIndex = 0;
                            }
                        break;

                        case 2:
                            if(AccessoryUpgradeIndex < Player.AccessoryUpgrades.Count - 1){
                                AccessoryUpgradeIndex += 1;
                            }
                            else{
                                AccessoryUpgradeIndex = 0;
                            }
                        break;
                    }
                    InputTimerCooldown = InputTimerCooldownAmount;
                    SetUpgradeInfo(ActiveSubMenu);
                    //Next Item
                }
                if(Input.GetButtonDown("Confirm")){
                    EquipUpgrade(ActiveSubMenu);
                    InUpgradeMenu = false;
                    UpgradePanel.SetActive(false);
                }
                if(Input.GetButtonDown("Cancel")){
                    InUpgradeMenu = false;
                    UpgradePanel.SetActive(false);
                }
            }
        }
        else{
            InputTimerCooldown -= Time.deltaTime;
        }
    }

    private bool CheckAvailableSlot(int activeSubMenu){
        switch(activeSubMenu){
            case 0: 
                if(Player.Weapons[WeaponIndex].UpgradeSlots > 0){
                    return true;
                }
                else{
                    return false;
                }
            case 1:
                if(Player.Armors[ArmorIndex].UpgradeSlots > 0){
                    return true;
                }
                else{
                    return false;
                }
            case 2:
                if(Player.Accessories[AccessoryIndex].UpgradeSlots > 0){
                    return true;
                }
                else{
                    return false;
                }
        }
        return false;
    }
    private bool CheckIfPlayerHasUpgrades(int activeSubMenu){
        switch(activeSubMenu){
            case 0: 
                if(Player.WeaponUpgrades.Count > 0){
                    return true;
                }
                else{
                    return false;
                }
            case 1:
                if(Player.ArmorUpgrades.Count > 0){
                    return true;
                }
                else{
                    return false;
                }
            case 2:
                if(Player.AccessoryUpgrades.Count > 0){
                    return true;
                }
                else{
                    return false;
                }
        }
        return false;
    }
    public void SetItemInfo(int subMenuIndex){
        switch(subMenuIndex){
            case 0:
                if(WeaponIndex < Player.Weapons.Count){
                    ItemName.text = Player.Weapons[WeaponIndex].Name;
                    //Item Image coming soon tm
                    ItemStats.text = Player.Weapons[WeaponIndex].Description;

                }
            break;
            case 1:
                if(Player.Armors[ArmorIndex].Name != null){
                    ItemName.text = Player.Armors[ArmorIndex].Name;
                    //Item Image blah blah
                    ItemStats.text = Player.Armors[ArmorIndex].Description;
                    if(Player.Armors[ArmorIndex].Upgrades.Count > 0){
                        UpgradeName.text = Player.Weapons[ArmorIndex].Upgrades[0].Name;
                        //Upgrade Image coming soon tm
                    }
                }
            break;
            case 2:
            if(Player.Accessories[AccessoryIndex].Name != null){
                ItemName.text = Player.Accessories[AccessoryIndex].Name;
                //Item Image blah blah
                ItemStats.text = Player.Accessories[AccessoryIndex].Description;
                if(Player.Accessories[AccessoryIndex].Upgrades.Count > 0){
                    UpgradeName.text = Player.Weapons[AccessoryIndex].Upgrades[0].Name;
                    //Upgrade Image coming soon tm
                }
            }
            break;
        }
    }
    public void SetUpgradeInfo(int subMenuIndex){
        switch(subMenuIndex){
            case 0:
                if(Player.WeaponUpgrades.Count > 0){
                    UpgradeName.text = Player.WeaponUpgrades[WeaponUpgradeIndex].Name;
                    UpgradeDesc.text = Player.WeaponUpgrades[WeaponUpgradeIndex].Description;
                }
            break;
            case 1:
                if(Player.ArmorUpgrades.Count > 0){
                    UpgradeName.text = Player.ArmorUpgrades[ArmorUpgradeIndex].Name;
                    UpgradeDesc.text = Player.ArmorUpgrades[ArmorUpgradeIndex].Description;
                }
            break;
            case 2:
                if(Player.WeaponUpgrades.Count > 0){
                    UpgradeName.text = Player.AccessoryUpgrades[AccessoryUpgradeIndex].Name;
                    UpgradeDesc.text = Player.AccessoryUpgrades[AccessoryUpgradeIndex].Description;
                }
            break;
        }
    }
    private void EquipUpgrade(int itemType){
        switch(itemType){
            case 0:
                Player.RemoveStatsFromEquipment(EquipmentType.Weapon);
                Player.Weapons[WeaponIndex].Upgrades.Add(Player.WeaponUpgrades[WeaponUpgradeIndex]);
                Player.WeaponUpgrades.RemoveAt(WeaponUpgradeIndex);
                Player.AddStatsFromEquipment(EquipmentType.Weapon);
            break;
            case 1: 
                Player.RemoveStatsFromEquipment(EquipmentType.Armor);
                Player.Armors[ArmorIndex].Upgrades.Add(Player.ArmorUpgrades[ArmorUpgradeIndex]);
                Player.ArmorUpgrades.RemoveAt(ArmorUpgradeIndex);
                Player.AddStatsFromEquipment(EquipmentType.Armor);
            break;
            case 2:
                Player.RemoveStatsFromEquipment(EquipmentType.Armor);
                Player.Accessories[ArmorIndex].Upgrades.Add(Player.AccessoryUpgrades[ArmorUpgradeIndex]);
                Player.AccessoryUpgrades.RemoveAt(ArmorUpgradeIndex);
                Player.AddStatsFromEquipment(EquipmentType.Accessory);
            break;
        }
    }
    private void GenerateMiniPanels(){
        for(int i = 0; i <= MiniUpgradePanelCount; i++){
            GameObject obj = (GameObject)Instantiate(MiniUpgradePanel);
            obj.transform.SetParent(MiniUpgradePanelParent.transform);
            // obj.GetComponentInChildren<Text>();
            // obj.GetComponentInChildren<Image>();
            obj.SetActive(false);
            MiniUpgradePanels.Add(obj);
        }
    }
    private void SetMiniPanelsInfo(int activeSubMenu){
        foreach(GameObject go in MiniUpgradePanels){
            go.GetComponentInChildren<Text>().text = "Empty";
            //Image
            go.SetActive(false);
        }
        switch(activeSubMenu){
            case 0:
                if(Player.Weapons[WeaponIndex].UpgradeSlots > 0){
                    for(int i = 0; i < Player.Weapons[WeaponIndex].UpgradeSlots; i++){
                        foreach(GameObject go in MiniUpgradePanels){
                            if(!go.activeInHierarchy){
                                go.transform.localScale = new Vector2(1, 1);
                                if(Player.Weapons[WeaponIndex].Upgrades.Count >= i + 1){
                                    go.GetComponentInChildren<Text>().text = Player.Weapons[WeaponIndex].Upgrades[i].Name;
                                }
                                // go.GetComponentInChildren<Image>().sprite = Player.Weapons[WeaponIndex].Upgrades[i].Image;
                                go.SetActive(true);
                                break;
                            }
                        }
                    }
                    // UpgradeName.text = Player.Weapons[WeaponIndex].Upgrades[0].Name;
                    //Upgrade Image coming soon tm
                }
            break;
            case 1:
                if(Player.Armors[ArmorIndex].UpgradeSlots > 0){
                    for(int i = 0; i < Player.Armors[ArmorIndex].UpgradeSlots; i++){
                        foreach(GameObject go in MiniUpgradePanels){
                            if(!go.activeInHierarchy){
                                go.transform.localScale = new Vector2(1, 1);
                                if(Player.Armors[ArmorIndex].Upgrades.Count >= i + 1){
                                    go.GetComponentInChildren<Text>().text = Player.Armors[ArmorIndex].Upgrades[i].Name;
                                }                                
                                go.SetActive(true);
                                break;
                            }
                        }
                    }
                    // UpgradeName.text = Player.Weapons[WeaponIndex].Upgrades[0].Name;
                    //Upgrade Image coming soon tm
                }
            break;
            case 2:
                if(Player.Accessories[AccessoryIndex].UpgradeSlots > 0){
                    for(int i = 0; i < Player.Accessories[AccessoryIndex].UpgradeSlots; i++){
                        foreach(GameObject go in MiniUpgradePanels){
                            if(!go.activeInHierarchy){
                                go.transform.localScale = new Vector2(1, 1);
                                if(Player.Accessories[AccessoryIndex].Upgrades.Count >= i + 1){
                                    go.GetComponentInChildren<Text>().text = Player.Accessories[AccessoryIndex].Upgrades[i].Name;
                                }                                
                                go.SetActive(true);
                                break;
                            }
                        }
                    }
                }
            break;
        }
    }
    private void EquipItem(int subMenuIndex){
        switch(subMenuIndex){
            case 0:
            // Player.RemoveStatsFromEquipment(EquipmentType.Weapon);
            Player.CurrentWeapon = WeaponIndex;
            // Player.AddStatsFromEquipment(EquipmentType.Weapon);
            Controls.MenuOpen = false;
            gameObject.SetActive(false);
            break;
            case 1:
            Player.RemoveStatsFromEquipment(EquipmentType.Armor);
            Player.CurrentArmor = ArmorIndex;
            Player.AddStatsFromEquipment(EquipmentType.Armor);
            Controls.MenuOpen = false;
            gameObject.SetActive(false);
            break;
            case 2: 
            Player.RemoveStatsFromEquipment(EquipmentType.Accessory);
            Player.CurrentAccessory = AccessoryIndex;
            Player.AddStatsFromEquipment(EquipmentType.Accessory);
            Controls.MenuOpen = false;
            gameObject.SetActive(false);
            break;
        }
    }
    private void DropItem(int subMenuIndex){
        GameObject prefab;
        switch(subMenuIndex){
            case 0:
                if(Player.Weapons.Count > 1){
                    prefab = GameObject.Instantiate(PickUpPrefab);
                    Weapon weapon = Player.Weapons[WeaponIndex];
                    Pickup pickup = prefab.GetComponent<Pickup>();
                    QuickItemPanel panel = pickup.ItemPanel.GetComponent<QuickItemPanel>();
                    pickup.PickupType = PickupType.Weapon;
                    pickup.Item = (Item)weapon;
                    panel.Item = pickup.Item;
                    pickup.transform.SetParent(Player.transform.parent);
                    pickup.transform.localScale = new Vector2(128, 128);
                    pickup.transform.position = new Vector2(Player.transform.position.x + 1, Player.transform.position.y);
                    Player.Weapons.RemoveAt(WeaponIndex);
                    WeaponIndex = 0;
                    SetItemInfo(subMenuIndex);
                }
            break;
            case 1:
                prefab = GameObject.Instantiate(PickUpPrefab);
                prefab.GetComponent<Pickup>().Item = (Armor)Player.Armors[ArmorIndex];
                GameObject.Instantiate(prefab);
                Player.Armors.RemoveAt(ArmorIndex);
            break;
            case 2:
                prefab = GameObject.Instantiate(PickUpPrefab);
                prefab.GetComponent<Pickup>().Item = (Accessory)Player.Accessories[AccessoryIndex];
                GameObject.Instantiate(prefab);
                Player.Accessories.RemoveAt(AccessoryIndex);
            break;
        }
    }
}
