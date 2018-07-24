using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cola : MonoBehaviour, IPointerDownHandler
{
    public float disapperingTime = 0.1f;
    AnimationHandler animationHandler;
    GameManager gameManager;
    private float parPosition = 1000;
    public bool correct = true;
    private float tolerance = 50f;

    public void OnPointerDown(PointerEventData _eventData)
    {
        animationHandler.ResetAnimation();
        animationHandler.state_disappear = true;
        animationHandler.time = disapperingTime;
        animationHandler.state_destroy = true;
        if (correct)
        {
            gameManager.score += gameManager.level;
            gameManager.MakeNewFloatingText("+" + gameManager.level);
        }
        else
            gameManager.life--;
    }

    public void Move(float speed)
    {
        Start();
        animationHandler.disX = -2 * (parPosition + Mathf.Sign(parPosition) * tolerance);
        animationHandler.time = Mathf.Abs(animationHandler.disX / speed);
        animationHandler.state_destroy = true;
        animationHandler.state_activated = true;
    }

    void Start () {
        animationHandler = gameObject.GetComponent<AnimationHandler>();
        gameManager = gameObject.GetComponentInParent<GameManager>();
        parPosition = gameObject.transform.GetComponentInParent<Transform>().localPosition.x;
	}
    
    void Update () {
		if(correct && (Mathf.Abs(gameObject.transform.localPosition.x) >= Mathf.Abs(parPosition) + tolerance))
        {
            gameManager.life--;
            Destroy(gameObject);
        }
	}
}
