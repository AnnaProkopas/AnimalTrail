using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRatingService
{
    private const string RatingRecordField = "ratingRecord";
    private const string RatingListCountField = "ratingListCount";
    private const string RatingDatePrefixField = "statsRecordDate";
    private const string RatingValuePrefixField = "statsRecordValue";
    
    public int GetRecordFoodCounter() 
    {
        return PlayerPrefs.GetInt(RatingRecordField, 0);
    }

    public void SetRecordFoodCounter(int value) 
    {
        PlayerPrefs.SetInt(RatingRecordField, value);
    }

    public void AddRecord(int value)
    {
        int count = PlayerPrefs.GetInt(RatingListCountField, 0);
        
        PlayerPrefs.SetInt(RatingValuePrefixField + count, value);
        PlayerPrefs.SetString(RatingDatePrefixField + count, DateTime.Now.ToString("dd.MM.yyyy"));
        PlayerPrefs.SetInt(RatingListCountField, count + 1);
    }

    public List<StatRecord> GetStatRecords()
    {
        List<StatRecord> list = new List<StatRecord>();
        int count = PlayerPrefs.GetInt(RatingListCountField, 0);
        for (int i = 0; i < count; i++)
        {
            StatRecord item = new StatRecord();
            item.value = PlayerPrefs.GetInt(RatingValuePrefixField + i);
            item.date = PlayerPrefs.GetString(RatingDatePrefixField + i);
            list.Add(item);
        }

        return list;
    }
}

public struct StatRecord
{
    public int value;
    public string date;
}