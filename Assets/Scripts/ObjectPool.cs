using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> PooledItems;
    public GameObject ItemToPool;
    public Transform ParentTransform;
    public int AmountToPool;
    public EquipmentGeneration EG;
    public void InstantiateBullets(int Amount){
        for(int i = 0; i <= Amount; i++){
            GameObject obj = (GameObject)Instantiate(ItemToPool);
            obj.transform.SetParent(ParentTransform);
            obj.SetActive(false);
            if(transform.tag == "Player"){
                obj.transform.tag = "PlayerBullet";
            }
            if(transform.tag == "Enemy"){
                obj.transform.tag = "EnemyBullet";
            }
            PooledItems.Add(obj);
        }
    }
    public void InstantiateObjects(int amount){
        for(int i = 0; i <= amount; i++){
            GameObject obj = (GameObject)Instantiate(ItemToPool);
            obj.transform.SetParent(ParentTransform);
            obj.SetActive(false);
            PooledItems.Add(obj);
        }
    }
}
