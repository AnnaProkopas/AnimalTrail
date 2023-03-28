using System;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Text text;
    [SerializeField] private PointVarController incObject;
    [SerializeField] private PointVarController decObject;

    public int value;

    public void AnimatedChange(int newValue, int variation)
    {
        if (variation > 0)
        {
            var copyObject = Instantiate(incObject, rectTransform.position, Quaternion.identity);
            copyObject.transform.parent = rectTransform;
            copyObject.UpdateText(variation.ToString());
        }
        else if (variation < 0)
        {
            var copyObject = Instantiate(decObject, rectTransform.position, Quaternion.identity);
            copyObject.transform.parent = gameObject.transform;
            copyObject.UpdateText((-1 * variation).ToString());
        }

        value = newValue;
        UpdateText();
    }

    public void HiddenChange(int newValue)
    {
        value = newValue;
        UpdateText();
    }
    private void UpdateText()
    {
        text.text = value.ToString();
    }
}
