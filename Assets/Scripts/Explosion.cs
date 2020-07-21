using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Vector2 Radius;
    public float LifeTime;
    public int Damage;
    public CircleCollider2D CC;
    public Rigidbody2D RB;
    public SpriteRenderer Sprite;

    void OnEnable()
    {
        gameObject.transform.localScale = Radius;
        StartCoroutine("Explode");
    }

    public IEnumerator Explode(){
        yield return new WaitForSeconds(LifeTime);
        gameObject.SetActive(false);
    }
}
