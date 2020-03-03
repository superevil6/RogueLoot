using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Player Player;
    public Rigidbody2D RB;
    private float TimeBetweenAttacks;
    // Start is called before the first frame update
    void Start()
    {
        TimeBetweenAttacks = Player.Weapon.Attack.AttackSpeed;
        print(TimeBetweenAttacks);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RB.velocity = new Vector2(
            Mathf.Lerp(0, Input.GetAxisRaw("HorizontalLeft") * Player.Speed, 0.8f), 
            Mathf.Lerp(0, Input.GetAxisRaw("VerticalLeft") * Player.Speed, 0.8f));
        if(TimeBetweenAttacks <= 0){
            if((Input.GetAxisRaw("HorizontalRight") == 1 || Input.GetAxisRaw("HorizontalRight") == -1)
            || (Input.GetAxisRaw("VerticalRight") == 1 || Input.GetAxisRaw("VerticalRight") == -1)){
                Vector2 shotDirection = new Vector2(
                    Input.GetAxisRaw("HorizontalRight"),// * Player.Weapon.Attack.Distance,
                    Input.GetAxisRaw("VerticalRight")// * Player.Weapon.Attack.Distance
                );
                int bulletsToShoot = Player.Weapon.Attack.Shots;
                foreach(GameObject go in Player.Bullets.PooledItems){
                    if(!go.activeInHierarchy && bulletsToShoot > 0){
                        go.transform.position = transform.position;
                        go.GetComponent<Bullet>().Direction = 
                            new Vector2((shotDirection.x * Player.Weapon.Attack.Distance) + RollAccuracy(Player.Weapon.Attack.Accuracy) + transform.position.x, 
                            (shotDirection.y * Player.Weapon.Attack.Distance) + RollAccuracy(Player.Weapon.Attack.Accuracy) + transform.position.y);
                        go.GetComponent<Bullet>().Attack = Player.Weapon.Attack;
                        go.SetActive(true);
                        bulletsToShoot -= 1;
                    }
                    TimeBetweenAttacks = Player.Weapon.Attack.AttackSpeed;
                }
            }
        }
        else{
            TimeBetweenAttacks -= Time.deltaTime;
        }
    }
    private float RollAccuracy(float accuracy){
        return Random.Range(-accuracy, accuracy);
    }
}
