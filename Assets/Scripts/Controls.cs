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
    private int JumpsRemaining;
    private float MovementInput;
    private bool Grounded;
    public Transform FeetPosition;
    public float CheckRadius;
    public LayerMask GroundIdentifier;
    private float JumptimeCounter;
    public float JumpTime;
    private bool IsJumping;
    // Start is called before the first frame update
    void Start()
    {
        TimeBetweenAttacks = Player.Weapons[Player.CurrentWeapon].Attack.AttackSpeed;
        JumpsRemaining = Player.JumpNumber;
        MenuOpen = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementInput = Input.GetAxisRaw("HorizontalLeft");
        RB.velocity = new Vector2(MovementInput * Player.Speed, RB.velocity.y);
        if(!MenuOpen){
            // RB.velocity = new Vector2(
            //     Mathf.Lerp(0, Input.GetAxisRaw("HorizontalLeft") * Player.Speed, 0.8f), 
            //     //Mathf.Lerp(0, Input.GetAxisRaw("VerticalLeft") * Player.Speed, 0.8f)
            //     -2.8f
            //     );

            if(TimeBetweenAttacks <= 0){
                if((Input.GetAxisRaw("HorizontalRight") >= 0.5f || Input.GetAxisRaw("HorizontalRight") <= -0.5f)
                || (Input.GetAxisRaw("VerticalRight") >= 0.5f || Input.GetAxisRaw("VerticalRight") <= -0.5f)){
                    Vector2 shotDirection = new Vector2(
                        Input.GetAxisRaw("HorizontalRight"),
                        Input.GetAxisRaw("VerticalRight")
                    );
                    int bulletsToShoot = Player.Weapons[Player.CurrentWeapon].Attack.Shots;
                    foreach(GameObject go in Player.Bullets.PooledItems){
                        if(!go.activeInHierarchy && bulletsToShoot > 0){
                            go.transform.position = transform.position;
                            go.GetComponent<Bullet>().Direction = 
                                new Vector2((shotDirection.x * Player.Weapons[Player.CurrentWeapon].Attack.Distance) + RollAccuracy(Player.Weapons[Player.CurrentWeapon].Attack.Accuracy) + transform.position.x, 
                                (shotDirection.y * Player.Weapons[Player.CurrentWeapon].Attack.Distance) + RollAccuracy(Player.Weapons[Player.CurrentWeapon].Attack.Accuracy) + transform.position.y);
                            go.GetComponent<Bullet>().Attack = Player.Weapons[Player.CurrentWeapon].Attack;
                            go.GetComponent<Bullet>().StartPosition = new Vector2(shotDirection.x / 2.5f  + transform.position.x, shotDirection.y / 2.5f + transform.position.y);
                            go.SetActive(true);
                            bulletsToShoot -= 1;
                        }
                        TimeBetweenAttacks = Player.Weapons[Player.CurrentWeapon].Attack.AttackSpeed;
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
        Grounded = Physics2D.OverlapCircle(FeetPosition.position, CheckRadius, GroundIdentifier);
        if(Grounded && JumpsRemaining < Player.JumpNumber){
            JumpsRemaining = Player.JumpNumber;
        }

        if(Input.GetButtonDown("Menu")){
            MenuOpen = !MenuOpen;
            RB.velocity = new Vector2(0, 0); //So the player doesn't keep moving when the menu is open.
            MenuPanel.SetActive(!MenuPanel.activeInHierarchy);
            MenuPanel.transform.position = Player.transform.position;
        }
        if(Input.GetButtonDown("L button") && Grounded){
            IsJumping = true;
            JumptimeCounter = JumpTime;
            RB.velocity = Vector2.up * Player.JumpHeight;
            JumpsRemaining -= 1;
        }
        if(Input.GetButton("L button") && IsJumping){
            if(JumptimeCounter > 0){
                RB.velocity = Vector2.up * Player.JumpHeight;
                JumptimeCounter -= Time.deltaTime;
            }
            else{
                IsJumping = false;
            }
        }
        if(Input.GetButtonUp("L button")){
            IsJumping = false;
        }
        if(Input.GetButtonDown("R button")){
            if(Input.GetAxisRaw("HorizontalLeft") > 0){
                print("right");
                RB.AddForce(-Vector2.left * 2500);
            }
            if(Input.GetAxisRaw("HorizontalLeft") < 0){
                print("left");
                RB.AddForce(Vector2.left * 2500);
            }
        }

        if(Input.GetButtonDown("Weapon Swap Left")){
            print("Swap Left");
            if(Player.CurrentWeapon > 0){
                Player.CurrentWeapon -= 1;
            }
            else{
                Player.CurrentWeapon = Player.Weapons.Count -1;
            }

        }
        if(Input.GetButtonDown("Weapon Swap Right")){
            print("Swap Right");
            if(Player.CurrentWeapon < Player.Weapons.Count - 1){
                Player.CurrentWeapon += 1;
            }
            else{
                Player.CurrentWeapon = 0;
            }
        }


    }
    private float RollAccuracy(float accuracy){
        return Random.Range(-accuracy, accuracy);
    }
}
