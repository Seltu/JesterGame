using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageBar : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private int maxValue;
    public GameObject[] uiBarImages;

    public void UpdateBar()
    {
        foreach (var image in uiBarImages)
        {
            image.SetActive(false);
        }
        var imageIndex = value / maxValue * (uiBarImages.Length - 1);
        uiBarImages[Mathf.FloorToInt(imageIndex)].SetActive(true);
    }

    public void SetValue(float newValue)
    {
        value = Mathf.Clamp(newValue, 0, maxValue);
        UpdateBar();
    }

    public void SetMaxValue(int newValue)
    {
        maxValue = newValue;
    }
}
