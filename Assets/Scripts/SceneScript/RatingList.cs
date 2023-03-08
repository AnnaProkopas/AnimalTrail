using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RatingList : MonoBehaviour
{
    [SerializeField] private Transform contentContainer;
    [SerializeField] private GameObject itemPrefab;

    private const string EmptyRating = "empty list";

    void Start()
    {
        PlayerRatingService ratingController = new PlayerRatingService();
        List <StatRecord> ratingList = ratingController.GetStatRecords();

        if (ratingList.Count == 0)
        {
            AddItem(EmptyRating);
        }
        else
        {
            for (int i = ratingList.Count - 1; i >= 0; i--)
            {
                Debug.Log(i);
                AddItem(ratingList[i].date + " " + ratingList[i].value);
            }
        }
    }

    private void AddItem(string text)
    {
        var item = Instantiate(itemPrefab);
        item.GetComponentInChildren<Text>().text = text;
        item.transform.SetParent(contentContainer);
        item.transform.localScale = Vector2.one;
    }
}
