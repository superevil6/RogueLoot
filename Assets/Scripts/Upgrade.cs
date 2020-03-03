using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "RogueLoot/Attack", order = 0)]
public class Upgrade : ScriptableObject
{
    public int Hits; //Like number of bullets
    public int DamageBoost;
    public int DefenseBoost;
    public Effect Effect;
}
