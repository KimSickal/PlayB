using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class scene0_start : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler {
    public void OnPointerDown(PointerEventData _eventData)
    {
        Debug.Log("꺄륵");
        UnityEngine.SceneManagement.SceneManager.LoadScene("s1_user");
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        Debug.Log("꺄륵");
    }
}
