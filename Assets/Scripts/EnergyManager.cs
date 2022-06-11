using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    //[SerializeField]
   // private int Energys = 10000;
    [SerializeField]
    private Text textEnergy;

    [SerializeField]
    private Text textTimer;

    [SerializeField]
    private Text maxEnergy;

    [SerializeField]
    public float EnergyAmount = 10;

    private int totalEnergy = 0;

    private DateTime nextEnergyTime;

    private DateTime lastAddedTime;

    private int RestoreDuration = 10; // 10 secconds for now


    
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    public void Load()
    {
      totalEnergy = PlayerPrefs.GetInt("totalEnergy");
      nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
      lastAddedTime = StringToDate(PlayerPrefs.GetString("LastAddedTime"));
    }

    public void Save()
    {
        PlayerPrefs.SetInt("TotalEnergy", totalEnergy);
        PlayerPrefs.SetString("nextenergyTime", nextEnergyTime.ToString());
        PlayerPrefs.SetString("lastAddedTime", lastAddedTime.ToString());

    }

    private DateTime StringToDate(string date)
    {
        if (string.IsNullOrEmpty(date))
            return DateTime.Now;

        return DateTime.Parse(date);
    }
}
