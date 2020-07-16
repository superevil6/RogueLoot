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
        Accessory,
        Upgrade
    }
    public enum Rarity{
        Normal,
        Magical, //Change the name of this, it makes no sense in a space enviornment
        Rare,
        Legendary
    }
    public enum Effect{
        Nothing,
        Burning,
        Freezing,
        Shocking,
        Poisoning,
        ShieldDraining,
        HealthDraining,
        Exploding
    }
    public enum PickupType{
        Weapon,
        Armor,
        Upgrade,
        Accessory,
        Money,
        Health
    }
    public enum EnemyAgression{
        Docile,
        Agressive,
        SuperAgressive
    }
    public enum EnemyAttackStyle{
        Melee,
        Range
    }
    public enum EnemyMovementStyle{
        Normal,
        Jumping,
        Stationary,
        Flying,
        Ground
    }
}

