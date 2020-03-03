using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "Weapon", menuName = "RogueLoot/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public string Name;
    public Rarity Rarity;
    public int Value;
    public Attack Attack;
    public int UpgradeSlots;
    public List<Upgrade> Upgrades = new List<Upgrade>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
