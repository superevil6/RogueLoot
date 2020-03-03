﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> PooledItems;
    public GameObject ItemToPool;
    public Transform ParentTransform;
    public void InstantiateObjects(int Amount){
        for(int i = 0; i <= Amount; i++){
            GameObject obj = (GameObject)Instantiate(ItemToPool);
            obj.transform.SetParent(ParentTransform);
            obj.SetActive(false);
            PooledItems.Add(obj);
        }
    }
}
