using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    public Attack Attack;
    void OnTriggerEnter2D(Collider2D other){
        if(other.transform.tag == "PlayerBullet"){
            CurrentHealth -= Mathf.RoundToInt(other.transform.GetComponent<Bullet>().TotalDamage - Defense);
            other.transform.gameObject.SetActive(false);
            print(CurrentHealth);
        }
    }
}
