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
    public GameObject InventoryPanel;
    public GameObject ItemPanel;
    public Image DetailImage;
    public Text DetailName;
    public Text DetailDescription;
    public EventSystem ES;
    private bool MenuOpen;
    private EquipmentType[] MenuOrder = new EquipmentType[]{EquipmentType.Weapon, EquipmentType.Armor, EquipmentType.Accessory};
    private int MenuIndex;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnEnable()
    {
        MakeInventory(Player.Inventory, EquipmentType.Weapon);
    }
    void OnDisable()
    {
        DestroyInventory();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("L button")){
            CycleMenuIndex(false);
            DestroyInventory();
            MakeInventory(Player.Inventory, MenuOrder[MenuIndex]);
            ES.SetSelectedGameObject(InventoryPanel.transform.GetChild(0).gameObject);
        }
        if(Input.GetButtonDown("R button")){
            CycleMenuIndex(true);
            DestroyInventory();
            MakeInventory(Player.Inventory, MenuOrder[MenuIndex]);
            ES.SetSelectedGameObject(InventoryPanel.transform.GetChild(0).gameObject);
        }

    }
    public void MakeInventory(List<Item> Inventory, EquipmentType subMenu){
        for(int i = 0; i < Inventory.Count; i++){
            if(Inventory[i].EquipmentType == subMenu){
                GameObject go = Instantiate(ItemPanel);
                go.transform.SetParent(InventoryPanel.transform);
                go.transform.localScale = go.transform.parent.localScale;
                go.GetComponent<ItemPanel>().Index = i;
                go.GetComponent<ItemPanel>().ET = subMenu;
                var text = go.GetComponentsInChildren<Text>();
                text[0].text = Inventory[i].Name;
                foreach(Effect effect in Inventory[i].Effects){
                    text[1].text += effect.Name;
                }
            }
        }
        ES.SetSelectedGameObject(InventoryPanel.transform.GetChild(0).gameObject);
        
    }
    public void UpdateDetailsScreen(){

    }
    public void DestroyInventory(){
        foreach(Transform child in InventoryPanel.transform){
            GameObject.Destroy(child.gameObject);
        }
    }
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


}
