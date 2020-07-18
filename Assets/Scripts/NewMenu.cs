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
    private int ActiveSubMenu = 0;
    private float InputTimerCooldown = 0;
    private float InputTimerCooldownAmount = 0.15f;
    //UI
    public Text ItemName;
    public Image ItemImage;
    public Text ItemStats;
    public Text UpgradeName;
    public Image UpgradeImage;

    // Start is called before the first frame update
    void Start()
    {
        ActiveSubMenu = 0;
        WeaponIndex = 0;
        ArmorIndex = 0;
        AccessoryIndex = 0;
        InputTimerCooldown = InputTimerCooldownAmount;
        SetItemInfo(ActiveSubMenu);

    }
    void OnEnable() {
        InputTimerCooldown = InputTimerCooldownAmount;
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = new Vector2(Player.transform.localPosition.x, Player.transform.localPosition.y + 200);
        if(InputTimerCooldown <= 0){
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
                InputTimerCooldown = InputTimerCooldownAmount;
                //Next Category
            }
            if(Input.GetButtonDown("Confirm")){
                switch(ActiveSubMenu){
                    case 0:
                    Player.RemoveStatsFromEquipment(EquipmentType.Weapon);
                    Player.CurrentWeapon = WeaponIndex;
                    Player.AddStatsFromEquipment(EquipmentType.Weapon);
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
        }
        else{
            InputTimerCooldown -= Time.deltaTime;
        }
    }

    public void SetItemInfo(int subMenuIndex){
        switch(subMenuIndex){
            case 0:
                if(WeaponIndex < Player.Weapons.Count){
                    ItemName.text = Player.Weapons[WeaponIndex].Name;
                    //Item Image coming soon tm
                    ItemStats.text = Player.Weapons[WeaponIndex].Description;
                    if(Player.Weapons[WeaponIndex].Upgrades.Count > 1){
                        UpgradeName.text = Player.Weapons[WeaponIndex].Upgrades[0].Name;
                        //Upgrade Image coming soon tm
                    }
                }
            break;
            case 1:
                if(Player.Armors[ArmorIndex].Name != null){
                    ItemName.text = Player.Armors[ArmorIndex].Name;
                    //Item Image blah blah
                    ItemStats.text = Player.Armors[ArmorIndex].Description;
                    if(Player.Armors[ArmorIndex].Upgrades.Count > 1){
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
                if(Player.Accessories[AccessoryIndex].Upgrades.Count > 1){
                    UpgradeName.text = Player.Weapons[AccessoryIndex].Upgrades[0].Name;
                    //Upgrade Image coming soon tm
                }
            }
            break;
        }
    }
}
