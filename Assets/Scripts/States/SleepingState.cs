using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;


public class SleepingState : StateBase
{
   //[SerializeField]
   // private int EnergyManager = 10;

    public float regenEnergySpeed = 4f;

    private void OnEnable()
    {
        //start lie down process
        transform.DORotate(new Vector3(0, 0, 90f), 2f);
    }

    private void OnDisable()
    {
        transform.DORotate(new Vector3(0, 0, 0f), 0.6f);
    }

    //update is called once per frame
    void update()
    {
        GetComponent<EnergyManager>().EnergyAmount += regenEnergySpeed * Time.deltaTime;
    }
}
