using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "Behavior", menuName = "RogueLoot/Behavior", order = 1)]

public class EnemyBehavior : ScriptableObject
{
    public Attack[] Attacks;
    public EnemyAgression Agression;
    public EnemyAttackStyle AttackStyle;
    public EnemyMovementStyle MovementStyle;
    public float ActionCooldown;
    public bool DropsItems;
    public float AgroDistance;
}
