using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStats : MonoBehaviour
{
    public Actor Actor;
    public int ColdThreshold = 5;
    public int ColdBuildup;
    public int FireThreshold = 5;
    public int FireBuildup;
    public int ElectricThreshold = 5;
    public int ElectricBuildup;
    public int PoisonThreshold = 5;
    public int PoisonBuildup;
    public int AcidThreshold = 5;
    public int AcidBuildup;
    private bool Burning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PoisonBuildup > PoisonThreshold){
            StartCoroutine("StartPoison");
            PoisonBuildup = 0;
        }
        if(ColdBuildup > ColdThreshold){
            StartCoroutine("StartCold");
            ColdBuildup = 0;
        }
        if(ElectricBuildup > ElectricThreshold){
            Actor.CurrentShield -= 100;
            ElectricBuildup = 0;
        }
        if(FireBuildup > FireThreshold){
            StartCoroutine("StartFire");
            FireBuildup = 0;
        }
        if(AcidBuildup > AcidThreshold){
            StartCoroutine("StartAcid");
            AcidBuildup = 0;
        }
        if(Burning){
            Actor.CurrentHealth -= 1;
        }
    }
    public IEnumerator StartPoison(){
        var initialPower = Actor.Power;
        var initialColor = Actor.SR.color;
        Actor.Power /= 2;
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
        Burning = true;
        Actor.SR.color = new Color32(100, 50, 0, 100);
        yield return new WaitForSeconds(5);
        Burning = false;
        Actor.SR.color = initialColor;
    }
    public IEnumerator StartAcid(){
        var initialColor = Actor.SR.color;
        var initialDefense = Actor.Defense;
        Actor.Defense /= 2;
        Actor.SR.color = new Color32(100, 50, 0, 100);
        yield return new WaitForSeconds(5);
        Actor.Defense =initialDefense;
        Actor.SR.color = initialColor;
    }
}
