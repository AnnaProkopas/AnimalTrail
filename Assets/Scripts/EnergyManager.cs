using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Text textEnergy;

    [SerializeField]
    private int maxEnergy;
    
    private int totalEnergy = 10;

    private DateTime nextEnergyTime;

    private DateTime lastAddedTime;

    private int restoreDuration = 6;

    private bool restoring=false;

    // Start is called before the first frame update
    void Start() {
        // Load();
        totalEnergy = 10;
        lastAddedTime = DateTime.Now;
        nextEnergyTime = AddDuration(DateTime.Now, restoreDuration * 2);

        StartCoroutine(RestoreRoutine());
    }

    public void AddEnergy(int value) {
        if (totalEnergy == maxEnergy) {
            return;
        }

        totalEnergy = Math.Min(totalEnergy + value, maxEnergy);
        UpdateEnergy();

        if (!restoring) {
            if (totalEnergy - 1 == 0) {
                // if energy is empty just now
                nextEnergyTime = AddDuration(DateTime.Now, restoreDuration);
            }
            StartCoroutine(RestoreRoutine());
        }
    }

    private IEnumerator RestoreRoutine() {
        UpdateEnergy();
        restoring = true;

        while (totalEnergy > 0) {
            DateTime currentTime = DateTime.Now;
            DateTime counter = nextEnergyTime;
            bool isAdding = false;
            while (currentTime > counter) {
                if (totalEnergy > 0) {
                    isAdding = true;
                    totalEnergy--;
                    DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                    counter = AddDuration(timeToAdd, restoreDuration);
                } else {
                    break;
                }
            }

            if (isAdding) {
                lastAddedTime = DateTime.Now;
                nextEnergyTime = counter;
            }

            UpdateEnergy();
            // Save();
            yield return null;
        }
        restoring = false;
    }

    private void UpdateEnergy() { 
        textEnergy.text = totalEnergy.ToString();
    }

    private DateTime AddDuration(DateTime time, int duration) {
        return time.AddSeconds(duration);
    }

    // private void Load() {
    //     totalEnergy = PlayerPrefs.GetInt("totalEnergy", maxEnergy);
    //     nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
    //     lastAddedTime = StringToDate(PlayerPrefs.GetString("lastAddedTime"));
    // }

    // private void Save() {
    //     PlayerPrefs.SetInt("totalEnergy", totalEnergy);
    //     PlayerPrefs.SetString("nextEnergyTime", nextEnergyTime.ToString());
    //     PlayerPrefs.SetString("lastAddedTime", lastAddedTime.ToString());
    // }

    private DateTime StringToDate(string date) {
        if (String.IsNullOrEmpty(date)) {
            return DateTime.Now;
        }
        return DateTime.Parse(date);
    }

    public int GetEnergyValue() {
        return totalEnergy;
    }
}
