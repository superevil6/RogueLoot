using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Player Player;
    public Rigidbody2D RB;
    public GameObject MenuPanel;
    private float TimeBetweenAttacks;
    private bool MenuOpen;
    // Start is called before the first frame update
    void Start()
    {
        TimeBetweenAttacks = Player.Weapon.Attack.AttackSpeed;
        MenuOpen = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!MenuOpen){
            RB.velocity = new Vector2(
                Mathf.Lerp(0, Input.GetAxisRaw("HorizontalLeft") * Player.Speed, 0.8f), 
                Mathf.Lerp(0, Input.GetAxisRaw("VerticalLeft") * Player.Speed, 0.8f));
            if(TimeBetweenAttacks <= 0){
                if((Input.GetAxisRaw("HorizontalRight") >= 0.5f || Input.GetAxisRaw("HorizontalRight") <= -0.5f)
                || (Input.GetAxisRaw("VerticalRight") >= 0.5f || Input.GetAxisRaw("VerticalRight") <= -0.5f)){
                    Vector2 shotDirection = new Vector2(
                        Input.GetAxisRaw("HorizontalRight"),
                        Input.GetAxisRaw("VerticalRight")
                    );
                    int bulletsToShoot = Player.Weapon.Attack.Shots;
                    foreach(GameObject go in Player.Bullets.PooledItems){
                        if(!go.activeInHierarchy && bulletsToShoot > 0){
                            go.transform.position = transform.position;
                            go.GetComponent<Bullet>().Direction = 
                                new Vector2((shotDirection.x * Player.Weapon.Attack.Distance) + RollAccuracy(Player.Weapon.Attack.Accuracy) + transform.position.x, 
                                (shotDirection.y * Player.Weapon.Attack.Distance) + RollAccuracy(Player.Weapon.Attack.Accuracy) + transform.position.y);
                            go.GetComponent<Bullet>().Attack = Player.Weapon.Attack;
                            go.GetComponent<Bullet>().StartPosition = new Vector2(shotDirection.x / 2.5f  + transform.position.x, shotDirection.y / 2.5f + transform.position.y);
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
    }
    void Update()
    {
        if(Input.GetButtonDown("Menu")){
            MenuOpen = !MenuOpen;
            RB.velocity = new Vector2(0, 0); //So the player doesn't keep moving when the menu is open.
            MenuPanel.SetActive(!MenuPanel.activeInHierarchy);
            MenuPanel.transform.position = Player.transform.position;
        }
    }
    private float RollAccuracy(float accuracy){
        return Random.Range(-accuracy, accuracy);
    }
}
