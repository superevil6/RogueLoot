using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStats : MonoBehaviour
{
    public Actor Actor;
    public int ColdThreshhold = 5;
    public int ColdBuildup;
    public int FireThreshhold = 5;
    public int FireBuildup;
    public int ElectricThreshhold = 5;
    public int ElectricBuildup;
    public int PoisonThreshhold = 5;
    public int PoisonBuildup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PoisonBuildup > PoisonThreshhold){
            StartCoroutine("StartPoison");
            PoisonBuildup = 0;
        }
        if(ColdBuildup > ColdThreshhold){
            StartCoroutine("StartCold");
            ColdBuildup = 0;
        }
        if(ElectricBuildup > ElectricThreshhold){
            Actor.CurrentShield -= 100;
            ElectricBuildup = 0;
        }
        if(FireBuildup > FireThreshhold){
            StartCoroutine("StartFire");
            FireBuildup = 0;
        }
    }
    public IEnumerator StartPoison(){
        var initialPower = Actor.Power;
        var initialColor = Actor.SR.color;
        Actor.Power -= Actor.Power/2;
        Actor.SR.color = new Color32(0, 50, 0, 100);
        yield return new WaitForSeconds(5);
        Actor.SR.color = initialColor;
        Actor.Power = initialPower;
    }
    public IEnumerator StartCold(){
        var initialSpeed = Actor.Speed;
        var initialColor = Actor.SR.color;
        Actor.Speed -= ColdBuildup / 100;
        Actor.SR.color = new Color32(80, 80, 100, 100);
        yield return new WaitForSeconds(5);
        Actor.Speed = initialSpeed;
        Actor.SR.color = initialColor;
    }
    public IEnumerator StartFire(){
        var initialColor = Actor.SR.color;
        Actor.CurrentHealth -= 2;
        Actor.SR.color = new Color32(100, 50, 0, 100);
        yield return new WaitForSeconds(5);
        Actor.SR.color = initialColor;
    }
}
