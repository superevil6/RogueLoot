using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Actor
{
    public Weapon Weapon;
    public Armor Armor;
    public List<Accessory> Accessories = new List<Accessory>();
    public Sprite Sprite;


    void Start()
    {
        Bullets.InstantiateObjects(NumberOfPooledBullets);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
