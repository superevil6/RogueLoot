using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "RogueLoot/Attack", order = 0)]
public class Attack : ScriptableObject
{
    public int BaseDamage;
    public float AttackSpeed;
    public float Distance;
    public float ShotSpeed;
    public int Shots;
    public float Accuracy;
    public float Size;
    public Effect Effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseAttack(){

    }
}
