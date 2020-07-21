using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Player Player;
    public Rigidbody2D RB;
    public GameObject MenuPanel;
    private float TimeBetweenAttacks;
    public bool MenuOpen;
    private int JumpsRemaining;
    private float MovementInput;
    private bool Grounded;
    private bool OnWallLeft;
    private bool OnWallRight;
    public Transform FeetPosition;
    public float CheckRadius;
    public LayerMask GroundIdentifier;
    public Transform LeftSide;
    public Transform RightSide;
    // public LayerMask WallIdentifier;
    private float JumptimeCounter;
    public float JumpTime;
    private bool IsJumping;
    private float DashTime;
    private float TempSpeed;
    private bool IsDashing;
    private bool IsWallJumping;
    private float WallJumpTimer;
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
        if(!IsWallJumping){
            MovementInput = Input.GetAxisRaw("HorizontalLeft");
            RB.velocity = new Vector2(MovementInput * Player.Speed, RB.velocity.y);
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
                            var bullet = go.GetComponent<Bullet>();
                            go.transform.position = transform.position;
                                bullet.Direction = 
                                new Vector2((shotDirection.x * Player.Weapons[Player.CurrentWeapon].Attack.Distance) 
                                + RollAccuracy(Player.Weapons[Player.CurrentWeapon].Attack.Accuracy) + transform.position.x, 
                                (shotDirection.y * Player.Weapons[Player.CurrentWeapon].Attack.Distance) 
                                + RollAccuracy(Player.Weapons[Player.CurrentWeapon].Attack.Accuracy) + transform.position.y);
                            bullet.Attack = Player.Weapons[Player.CurrentWeapon].Attack;
                            bullet.StartPosition = new Vector2(shotDirection.x / 2.5f  
                            + transform.position.x, shotDirection.y / 2.5f + transform.position.y);
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
        OnWallLeft = Physics2D.OverlapCircle(LeftSide.position, CheckRadius, GroundIdentifier);
        OnWallRight = Physics2D.OverlapCircle(RightSide.position, CheckRadius, GroundIdentifier);

        if(Grounded || OnWallLeft || OnWallRight){
            JumpsRemaining = Player.JumpNumber;
        }

        if((Input.GetAxis("Menu Horizontal") > 0
        || Input.GetAxis("Menu Horizontal") < 0
        || Input.GetAxis("Menu Vertical") > 0
        || Input.GetAxis("Menu Vertical") < 0)
        && !MenuOpen
        ){
            MenuOpen = true;
            // RB.velocity = new Vector2(0, 0); //So the player doesn't keep moving when the menu is open.
            MenuPanel.SetActive(!MenuPanel.activeInHierarchy);
            // MenuPanel.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 1) ;
        }
        if(Input.GetButtonDown("L button") && !OnWallLeft && !OnWallRight && !IsJumping){
            if(JumpsRemaining > 0 || Grounded){
                IsJumping = true;
                JumptimeCounter = JumpTime;
                RB.velocity = Vector2.up * Player.JumpHeight;
                JumpsRemaining -= 1;
            }
        }

        #region WallJumping
        if(Input.GetButtonDown("L button") && OnWallLeft){
            WallJump(new Vector2(1, 1) * Player.JumpHeight * 1.5f);
        }        
        if(Input.GetButtonDown("L button") && OnWallRight){
            WallJump(new Vector2(-1, 1) * Player.JumpHeight * 1.5f);
        }
        if(IsWallJumping){
            WallJumpTimer -= Time.deltaTime;
        }
        if(IsWallJumping && WallJumpTimer <= 0){
            IsWallJumping = false;
        }
        #endregion

        if(JumptimeCounter > 0){
            JumptimeCounter -= Time.deltaTime;
        }
        else{
            IsJumping = false;
        }
        if(Input.GetButtonUp("L button")){
            IsJumping = false;
        }
        if(Input.GetButton("L button") && IsJumping){
            RB.velocity = Vector2.up * Player.JumpHeight;
        }

        if(Input.GetButtonDown("R button") && Grounded){
            if(DashTime <= 0){
                IsDashing = true;
                TempSpeed = Player.Speed;
                Player.Speed = Player.Speed * 2f;
                DashTime = 0.2f;
            }
        }

        // if(Input.GetButtonDown("Weapon Swap Left")){
        //     if(Player.CurrentWeapon > 0){
        //         Player.CurrentWeapon -= 1;
        //     }
        //     else{
        //         Player.CurrentWeapon = Player.Weapons.Count -1;
        //     }

        // }
        // if(Input.GetButtonDown("Weapon Swap Right")){
        //     if(Player.CurrentWeapon < Player.Weapons.Count - 1){
        //         Player.CurrentWeapon += 1;
        //     }
        //     else{
        //         Player.CurrentWeapon = 0;
        //     }
        // }

        if(DashTime > 0){
            DashTime -= Time.deltaTime;
        }
        if(IsDashing && DashTime <= 0){
            if(Grounded || OnWallLeft || OnWallRight){
                Player.Speed = TempSpeed;
                IsDashing = false;
            }
        }
    }
    private float RollAccuracy(float accuracy){
        return Random.Range(-accuracy, accuracy);
    }
    private void WallJump(Vector2 direction){
        WallJumpTimer = 0.15f;
        IsWallJumping = true;
        JumptimeCounter = JumpTime;
        RB.velocity = direction;
        JumpsRemaining -= 1;
    }
}
