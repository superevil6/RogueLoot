using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int TotalHealth;
    public int CurrentHealth;
    public int TotalShield;
    public int CurrentShield;
    public float Power; //Affects attack power.
    public float Defense; 
    public float Speed; //Movement Speed
    public ObjectPool Bullets;
    public int NumberOfPooledBullets;


}
