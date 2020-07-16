using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Enemy : Actor
{
    public EnemyBehavior EBehavior;
    private float CurrentCooldown;

    void Start(){
        Bullets.InstantiateObjects(NumberOfPooledBullets);
    }

    void Update(){
        if(CurrentCooldown <= 0){
            DetermineAction();
            CurrentCooldown = EBehavior.ActionCooldown;
            //Do action
        }
        else{
            CurrentCooldown -= Time.deltaTime;
        }
    }
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
        }
    }

    public void DetermineAction(){
        //make this more elaborate
        int action = 0;//= Random.Range(0, 2);
        var player = GameObject.FindGameObjectWithTag("Player");
        if(action == 0){
            //attack
            Attack chosenAttack = PickAttack(EBehavior.Attacks);
            //use attack
            PerformAttack(chosenAttack, player);

        }
        else{
            //move
            Vector2 playerLocation = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);
            this.transform.localPosition = new Vector2(playerLocation.x + 5, playerLocation.y + 1);
        }
    }
    public void DetermineMovement(EnemyMovementStyle EMS){
        switch(EMS){
            case EnemyMovementStyle.Normal:
            //move towards player, consider jumping
            break;
            case EnemyMovementStyle.Jumping:
            //move towards player, prioritize jumping
            break;
            case EnemyMovementStyle.Flying:
            //Fly Towards enemy
            break;
            case EnemyMovementStyle.Ground:
            //MoveTowards Enemy, don't leave ground.
            break;
            case EnemyMovementStyle.Stationary:
            //Doesn't move.
            break;
        }
    }

    public Attack PickAttack(Attack[] attacks){
        if(attacks.Length > 1){
            return attacks[Random.Range(0, attacks.Length)];
        }
        else{
            return attacks[0];
        }
    }

    public void PerformAttack(Attack attack, GameObject player){
        int bulletsToShoot = attack.Shots;
        foreach(GameObject go in Bullets.PooledItems){
            if(!go.activeInHierarchy && bulletsToShoot > 0){
                var bullet = go.GetComponent<Bullet>();
                go.transform.position = transform.position;
                    bullet.Direction = 
                    new Vector2((player.transform.position.x) + RollAccuracy(attack.Accuracy), 
                    (player.transform.position.y) 
                    + RollAccuracy(attack.Accuracy));
                bullet.Attack = attack;
                bullet.StartPosition = 
                new Vector2(player.transform.position.x / 2.5f + transform.position.x, 
                player.transform.position.y / 2.5f + transform.position.y);
                go.SetActive(true);
                bulletsToShoot -= 1;
            }
        }
    }
    private float RollAccuracy(float accuracy){
        return Random.Range(-accuracy, accuracy);
    }
}
