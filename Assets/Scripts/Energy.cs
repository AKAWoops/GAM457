using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    //[SerializeField]
   // private int Energys = 10000;
    [SerializeField]
    private int Timer = 30;
    [SerializeField]
    public float EnergyAmount = 10;

    private int totalEnergy = 0;

    private DateTime nextEnergyTime;

    private DateTime lastAddedTime;

    private int EnergyRestoreDuration = 10; // 10 secconds for now


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Load()
    {

    }

    public void Save()
    {
        PlayerPrefs.SetInt("TotalEnergy", totalEnergy);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
