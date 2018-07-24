using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class toggleButton : MonoBehaviour, IPointerDownHandler
{
    public bool isOn = false;

    public UnityEngine.UI.Image image;
    public Sprite onImg;
    public Sprite offImg;

    public void OnPointerDown(PointerEventData _eventData)
    {
        isOn = !isOn;
        if (isOn)
            image.sprite = onImg;
        else
            image.sprite = offImg;

            
           
            

    }

    private void Start()
    {
        if (isOn)
            image.sprite = onImg;
        else
            image.sprite = offImg;
    }
}
