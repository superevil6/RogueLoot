using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Attack", menuName = "RogueLoot/Attack", order = 0)]
public class Attack : ScriptableObject
{
    public string AttackName;
    public Sprite Sprite;
    public RuntimeAnimatorController RAC;
    public PhysicsMaterial2D BounceMaterial;
    public PhysicsMaterial2D RicochetMaterial;
    public int BaseDamage;
    public float AttackSpeed;
    public float Distance;
    public float ShotSpeed;
    public int Shots;
    public float Accuracy;
    public float Size;
    public int Pierces;
    public int ColdDamage;
    public int FireDamage;
    public int ElectricDamage;
    public int PoisonDamage;
    public int AcidDamage;
    public float ExplosionRadius;
    public float ExplosionTime;
    public float WaveDistance;
    public bool Ricochet;
    public float LobbingForce;
    public bool WallPiercing;
    public float EnemySeekingPull;
    public float MineLifeTime;
    public int SplitAmount;
    public float SplitDistance;
}
