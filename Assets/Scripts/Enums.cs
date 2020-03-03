using System.Collections;
using System.Collections.Generic;

namespace Enums{
    public enum UpgradeType{
        Weapon,
        Armor,
        Accessory
    }
    public enum EquipmentType{
        Weapon,
        Armor,
        Accessory
    }
    public enum Rarity{
        Normal,
        Magical, //Change the name of this, it makes no sense in a space enviornment
        Rare,
        Legendary
    }
    public enum Effect{
        Burning,
        Freezing,
        Shocking,
        Poisoning,
        ShieldDraining,
        HealthDraining,
        Exploding
    }
}

