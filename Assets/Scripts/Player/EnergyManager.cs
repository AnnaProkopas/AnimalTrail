using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] 
    private PlayerController player;
    
    [SerializeField]
    private Text textEnergy;
    [SerializeField]
    private int maxEnergy;
    
    private int totalEnergy = 10;

    private int TotalEnergy
    {
        get => totalEnergy;
        set
        {
            totalEnergy = value;
            if (totalEnergy == 0)
            {
                player.OnEnergyIsOver();
            }
        }
    }

    private DateTime nextEnergyTime;

    private DateTime lastChangedTime;

    private readonly int restoreDuration = 6;

    private bool restoring = false;

    private void Start() 
    {
        // Load();
        TotalEnergy = maxEnergy;
        lastChangedTime = DateTime.Now;
        nextEnergyTime = AddDuration(DateTime.Now, restoreDuration * 2);

        StartCoroutine(RestoreRoutine());
    }

    private IEnumerator RestoreRoutine() 
    {
        UpdateEnergy();
        restoring = true;

        while (TotalEnergy > 0) 
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = nextEnergyTime;
            bool isAdding = false;
            while (currentTime > counter) 
            {
                if (TotalEnergy > 0) 
                {
                    isAdding = true;
                    TotalEnergy--;
                    DateTime timeToSub = lastChangedTime > counter ? lastChangedTime : counter;
                    counter = AddDuration(timeToSub, restoreDuration);
                } else 
                {
                    break;
                }
            }

            if (isAdding) 
            {
                lastChangedTime = DateTime.Now;
                nextEnergyTime = counter;
            }

            UpdateEnergy();
            // Save();
            yield return null;
        }
        restoring = false;
    }

    private void UpdateEnergy() 
    { 
        textEnergy.text = TotalEnergy.ToString();
    }

    private DateTime AddDuration(DateTime time, int duration) 
    {
        return time.AddSeconds(duration);
    }

    // private void Load() {
    //     TotalEnergy = PlayerPrefs.GetInt("TotalEnergy", maxEnergy);
    // }

    // private void Save() {
    //     PlayerPrefs.SetInt("TotalEnergy", TotalEnergy);
    // }

    public void Add(int value) 
    {
        if (TotalEnergy == maxEnergy) 
        {
            return;
        }

        TotalEnergy = Math.Min(TotalEnergy + value, maxEnergy);
        UpdateEnergy();
    }

    public void Subtract(int value) 
    {
        if (TotalEnergy == 0) 
        {
            return;
        }

        TotalEnergy = Math.Max(TotalEnergy - value, 0);
        UpdateEnergy();

        if (!restoring) 
        {
            if (TotalEnergy == 1) 
            {
                // Add time to life so as not to die from the timer immediately after dealing damage
                nextEnergyTime = AddDuration(DateTime.Now, restoreDuration);
            }
            StartCoroutine(RestoreRoutine());
        }
    }

    public int GetEnergyValue() 
    {
        return TotalEnergy;
    }
}
