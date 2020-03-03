using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public SpriteRenderer SR;
    public BoxCollider2D BC;
    public Rigidbody2D RB;
    public int TotalHealth;
    public int CurrentHealth;
    public int TotalShield;
    public int CurrentShield;
    public float Power; //Affects attack power.
    public float Defense; 
    public float Speed; //Movement Speed
    public ObjectPool Bullets;
    public int NumberOfPooledBullets;

    void Start()
    {
        BC.size = new Vector2(SR.size.x, SR.size.y);
        CurrentHealth = TotalHealth;
        CurrentShield = TotalShield;

    }
    void FixedUpdate() {
        if(CurrentHealth <= 0){
            gameObject.SetActive(false);
        }
    }

}