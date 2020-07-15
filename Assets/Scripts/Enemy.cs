using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    public Attack Attack;
    void OnTriggerEnter2D(Collider2D other){
        if(other.transform.tag == "PlayerBullet"){
            var bullet = other.transform.GetComponent<Bullet>();
            CurrentHealth -= Mathf.RoundToInt(bullet.TotalDamage - Defense);
            if(bullet.Attack.ColdDamage >= 0){
                EffectStats.ColdBuildup += bullet.Attack.ColdDamage;
            }
            if(bullet.Attack.FireDamage >= 0){
                EffectStats.FireBuildup += bullet.Attack.FireDamage;
            }
            if(bullet.Attack.ElectricDamage >= 0){
                EffectStats.ElectricBuildup += bullet.Attack.ElectricDamage;
            }
            if(bullet.Attack.PoisonDamage >= 0){
                EffectStats.PoisonBuildup += bullet.Attack.PoisonDamage;
            }
            other.transform.gameObject.SetActive(false);
            print(CurrentHealth);
        }
    }
}
