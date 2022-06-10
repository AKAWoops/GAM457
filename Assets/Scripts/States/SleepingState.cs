using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingState : StateBase
{
    public float regenEnergySpeed = 4f;

    private void OnEnable()
    {
        //start lie down process
        transform.DORotate(new Vector3(0, 0, 90f), 2f);
    }

    private void OnDisable()
    {
        transform.DOROtate(new Vector3(0, 0, 0f), 0.6f);
    }

    //update is called once per frame
    void update()
    {
        GetComponent<Energy>().energyAmount += regenEnergySpeed * Time.deltaTime;
    }
}
