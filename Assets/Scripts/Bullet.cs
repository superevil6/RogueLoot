using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Attack Attack;
    public SpriteRenderer SpriteRenderer;
    public BoxCollider2D BC;
    public Rigidbody2D RB;
    public Vector3 Direction;
    public Vector2 StartPosition;
    public Animator Animator;
    public RuntimeAnimatorController AnimationController;
    private float LifeTime;
    private float startTime;
    private float journeyLength;
    public int TotalDamage;

    void OnEnable() {
        var actor = gameObject.GetComponentInParent<Actor>();
        SpriteRenderer.sprite = Attack.Sprite;
        Animator.runtimeAnimatorController = Attack.RAC;
        float angle = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg;
        //float rotation = angle + Camera.main.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(0, 0, -angle);
        if(actor != null){
            TotalDamage = Mathf.RoundToInt(Attack.BaseDamage * actor.Power);    
        }
        transform.localScale = new Vector2(1 * Attack.Size, 1 * Attack.Size);
        LifeTime = Attack.Distance;
        startTime = Time.time;
        journeyLength = Vector2.Distance(StartPosition, Direction);
    }
    
    void Update()
    {
        if(LifeTime > 0){
            float distanceCovered = (Time.time - startTime) * Attack.ShotSpeed;
            float fracJourney = distanceCovered / journeyLength;
            transform.position = Vector2.Lerp(StartPosition, Direction, fracJourney);
            LifeTime -= Time.deltaTime;
        }
        if(LifeTime <= 0){
            gameObject.SetActive(false);
        }
        if(transform.position == Direction){
            gameObject.SetActive(false);
        }
    }
}
 