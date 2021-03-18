using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Attack Attack;
    public SpriteRenderer SpriteRenderer;
    public CircleCollider2D CC;
    public Rigidbody2D RB;
    public Vector3 Direction;
    public Vector2 StartPosition;
    public Animator Animator;
    public RuntimeAnimatorController AnimationController;
    private float LifeTime;
    private float startTime;
    private float journeyLength;
    public int TotalDamage;
    public int SplitCount;
    public int PierceCount;
    public ObjectPool ExplosionPool;
    public GameObject ExplosionPrefab;
    private GameObject Explosion;

    void Awake(){
        ExplosionPool = GameObject.FindGameObjectWithTag("Explosion Pool").GetComponent<ObjectPool>();
        // Explosion = Instantiate(ExplosionPrefab);
        // var explosion = Explosion.GetComponent<Explosion>();
        // explosion.Radius = new Vector2(ExplosionRadius, ExplosionRadius);
        // explosion.LifeTime = ExplosionTime;
    }
    void OnEnable() {
        var actor = gameObject.GetComponentInParent<Actor>();
        foreach(GameObject go in ExplosionPool.PooledItems){
            if(!go.activeInHierarchy){
                Explosion = go;
            } 
        }
        if(Attack.EnemySeekingPull > 0){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if(Attack.LobbingForce > 0){
            CC.sharedMaterial = Attack.BounceMaterial;
        }
        if(Attack.Ricochet){
            CC.sharedMaterial = Attack.RicochetMaterial;
        }
        // Explosion.SetActive(false);
        // CC.enabled = true;
        SpriteRenderer.sprite = Attack.Sprite;
        Animator.runtimeAnimatorController = Attack.RAC;
        if(Attack.ExplosionRadius > 0){
            var explosion = Explosion.GetComponent<Explosion>();
            explosion.Radius = new Vector2(Attack.ExplosionRadius, Attack.ExplosionRadius);
            explosion.Damage = TotalDamage / 2;
        }
        float angle = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, -angle);
        if(actor != null){
            TotalDamage = Mathf.RoundToInt(Attack.BaseDamage * actor.Power);    
        }
        transform.localScale = new Vector2(1 * Attack.Size, 1 * Attack.Size);
        LifeTime = Attack.Distance;
        startTime = Time.time;
        journeyLength = Vector2.Distance(StartPosition, Direction);
        if(Attack.LobbingForce > 0){
            RB.AddForce(Direction * Attack.LobbingForce);
        }
    }
    
    void Update()
    {
        if(LifeTime > 0){
            if(Attack.LobbingForce == 0){
                float distanceCovered = (Time.time - startTime) * Attack.ShotSpeed;
                float fracJourney = distanceCovered / journeyLength;
                transform.position = Vector2.Lerp(StartPosition, Direction, fracJourney);
            }
            LifeTime -= Time.deltaTime;
        }
        if(LifeTime <= 0){
            gameObject.SetActive(false);
        }
        if(transform.position == Direction){
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Wall" && !Attack.WallPiercing && Attack.LobbingForce == 0){
            StopAllCoroutines();
            if(Attack.ExplosionRadius > 0){
                Explode();
            }
            else{
                gameObject.SetActive(false);    
            }
        }
    }
    public void Explode(){
        Explosion.transform.position = gameObject.transform.position;
        Explosion.SetActive(true);
        gameObject.SetActive(false);
    }
    public IEnumerator Hit(){
        //Play the animation and wait for the end of the animation.
        if(PierceCount > 0){
            PierceCount -= 1;
        }
        //If exploding, spawn explosion.
        else{
            CC.enabled = false;
            transform.position = Direction;
            yield return new WaitForSeconds(1);
        }
        gameObject.SetActive(false);
    }
}
 