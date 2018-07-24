using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tapToLoad: MonoBehaviour, IPointerDownHandler
{
    public string sceneName = "s1_user";
    public void OnPointerDown(PointerEventData _eventData)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != sceneName)
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        else
            Debug.Log("current scene");
    }
}
