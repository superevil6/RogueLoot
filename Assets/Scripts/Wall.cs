using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Rigidbody2D RB;
    public BoxCollider2D BC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     print("Touching Wall");
    //     other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    // }
}
