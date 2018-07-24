using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseUp : MonoBehaviour, IPointerDownHandler {
    public GameObject pauseScreen;
    public bool reverse = false;
    public void OnPointerDown(PointerEventData _eventData)
    {

        pauseScreen.SetActive(!reverse);
        Time.timeScale = reverse ? 1 : 0;


    }
}
