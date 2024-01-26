using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBar : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private int maxValue;
    [SerializeField] private Image barImage;
    public GameObject[] uiBarImages;

    public void UpdateBar()
    {
        foreach (var image in uiBarImages)
        {
            image.SetActive(false);
        }
        var imageIndex = value / maxValue * (uiBarImages.Length - 1);
        uiBarImages[Mathf.FloorToInt(imageIndex)].SetActive(true);
        barImage.fillAmount = value / maxValue;
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
