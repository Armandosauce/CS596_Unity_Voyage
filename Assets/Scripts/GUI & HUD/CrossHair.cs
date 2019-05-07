using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Texture2D image;
    [Range(0, 1)]
    public float yOffsetPercentage;
    [Range(0, 1)]
    public float imageAlpha;

    private float offsetY;
    private float prevAlpha;

    private void Awake()
    {
        prevAlpha = imageAlpha;
        image.SetPixels(modifyAlpha(image.GetPixels(), imageAlpha));
    }

    private void OnGUI()
    {
        if(imageAlpha != prevAlpha)
        {
            prevAlpha = imageAlpha;
            image.SetPixels(modifyAlpha(image.GetPixels(), imageAlpha));
        }

        float xMin = (Screen.width / 2) - (image.width / 2);
        float yMin = (Screen.height / 2) - (image.height / 2);
        offsetY = yMin * yOffsetPercentage;
        yMin = yMin - offsetY;
        
        GUI.DrawTexture(new Rect(xMin, yMin, image.width, image.height), image);
    }

    private Color[] modifyAlpha(Color[] colors, float alpha)
    {
        for(int i = 0; i < colors.Length; i++)
        {
            colors[i].a = alpha;
        }
        return colors;
    }
    
}
