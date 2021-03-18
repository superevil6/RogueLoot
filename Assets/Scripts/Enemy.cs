using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Enemy : Actor
{
    public EnemyBehavior EBehavior;
    private float CurrentCooldown;
    private bool Moving;
    private Vector2 Destination;
    private GameObject Player;

    void Start(){
        Bullets.InstantiateBullets(NumberOfPooledBullets);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){

        if(Vector2.Distance(transform.localPosition, Player.transform.localPosition) < EBehavior.AgroDistance){
            Destination = DetermineMovement(EBehavior.MovementStyle, EBehavior.AttackStyle, Player);
            transform.localPosition = Vector2.Lerp(transform.localPosition, Destination, Time.deltaTime * Speed);
            if(CurrentCooldown <= 0){
                if(Player.transform.position.y > Destination.y){
                    Jump();
                }
                DetermineAction();
                CurrentCooldown = EBehavior.ActionCooldown;
                //Do action
            }
            else{
                CurrentCooldown -= Time.deltaTime;
            }
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
            if(bullet.Attack.AcidDamage >= 0){
                EffectStats.AcidBuildup += bullet.Attack.AcidDamage;
            }
            if(bullet.Attack.ExplosionRadius > 0){
                bullet.Explode();
            }
            else{
                bullet.StartCoroutine("Hit");
            }
            // other.transform.gameObject.SetActive(false);
        }
    }

    public void DetermineAction(){
        int action = 0;//= Random.Range(0, 2);
        if(action == 0){
            //attack
            Attack chosenAttack = PickAttack(EBehavior.Attacks);
            //use attack
            PerformAttack(chosenAttack, Player);

        }
        else{
            //move
            Vector2 playerLocation = new Vector2(Player.transform.localPosition.x, Player.transform.localPosition.y);
            this.transform.localPosition = new Vector2(playerLocation.x + 5, playerLocation.y + 1);
        } 
    }
    public Vector2 DetermineMovement(EnemyMovementStyle EMS, EnemyAttackStyle EAS, GameObject player){
        if(EAS == EnemyAttackStyle.Melee){
            if(transform.localPosition.x > player.transform.localPosition.x){
                //The enemy is to the right of the player, so it wants to get in front of it.
                return new Vector2(player.transform.localPosition.x + 10, player.transform.localPosition.y);
            }
            else{
                //The enemy is to the left of the player, and wants to be behind it.
                return new Vector2(player.transform.localPosition.x - 10, player.transform.localPosition.y);
            }
        }
        if(EAS == EnemyAttackStyle.Range){
            if(transform.localPosition.x > player.transform.localPosition.x){
                //The enemy is to the right of the player, so it wants to get in front of it.
                return new Vector2(player.transform.localPosition.x + EBehavior.AgroDistance / 2, player.transform.localPosition.y);
            }
            else{
                //The enemy is to the left of the player, and wants to be behind it.
                return new Vector2(player.transform.localPosition.x - EBehavior.AgroDistance / 2, player.transform.localPosition.y);
            }
        }
        else{
            return transform.localPosition;
        }
        // switch(EMS){
        //     case EnemyMovementStyle.Normal:
        //     //move towards player, consider jumping
        //     break;
        //     case EnemyMovementStyle.Jumping:
        //     //move towards player, prioritize jumping
        //     break;
        //     case EnemyMovementStyle.Flying:
        //     //Fly Towards enemy
        //     break;
        //     case EnemyMovementStyle.Ground:
        //     //MoveTowards Enemy, don't leave ground.
        //     break;
        //     case EnemyMovementStyle.Stationary:
        //     //Doesn't move.
        //     break;
        // }
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
                new Vector2((player.transform.position.x) + RollAccuracy(attack.Accuracy), (player.transform.position.y) + RollAccuracy(attack.Accuracy));
                bullet.Attack = attack;
                bullet.StartPosition = transform.position;
                go.SetActive(true);
                bulletsToShoot -= 1;
            }
        }
    }
    private float RollAccuracy(float accuracy){
        return Random.Range(-accuracy, accuracy);
    }

    private void Jump(){
        RB.AddForce(Vector2.up * 50);
    }
}
