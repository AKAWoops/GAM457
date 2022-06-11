using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    [SerializeField]
    private int Energys = 10000;

    [SerializeField]
    public float EnergyAmount = 10;

    private int totalEnergy = 0;

    // Start is called before the first frame update
    void Start()
    {

    }
}
   