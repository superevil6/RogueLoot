using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectPool PickupPool;
    public ObjectPool ExplosionPool;
    // Start is called before the first frame update
    void Start()
    {
        PickupPool.InstantiateObjects(PickupPool.AmountToPool);
        ExplosionPool.InstantiateObjects(ExplosionPool.AmountToPool);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
