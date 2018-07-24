using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationHandler : MonoBehaviour{ 
    public UnityEngine.UI.Image Image;
    public UnityEngine.UI.Text Text;

    private bool isText = false;
    

    public bool state_activated = false;
    public bool state_disappear = false;
    public bool state_destroy = false;

    public float opacity = 1;

    public float disX = 0;
    public float disY = 0;
    public float time = 0;

    private float moveX = 0;
    private float moveY = 0;

    private void Start()
    {
        if(Image != null)
        {
            opacity = Image.color.a;
            isText = false;
        }
        else if(Text != null)
        {
            opacity = Text.color.a;
            isText = true;
        }
        else
        {
            state_disappear = false;
        }
    }

    public void ResetAnimation()
    {
        if (Image != null)
        {
            opacity = Image.color.a;
            isText = false;
        }
        else if (Text != null)
        {
            opacity = Text.color.a;
            isText = true;
        }
        else
        {
            state_disappear = false;
        }
        disX = 0;
        disY = 0;
        time = 0;

        moveX = 0;
        moveY = 0;
}

    private float regulate(float op)
    {
        return Mathf.Min(1, (Mathf.Max(0, op)));
    }

    private Color changeOpacity(Color clr, float op)
    {
        return new Color(clr.r, clr.g, clr.b, regulate(op));
    }


    private void Update()
    {
        if (state_activated)
        {
            if(time <= Time.deltaTime)
            {
                moveX = disX;
                moveY = disY;
                disX = 0;
                disY = 0;
                time = 0;
                gameObject.transform.Translate(moveX, moveY, 0);
                state_activated = false;
                if (state_destroy)
                {
                    Destroy(gameObject);
                }
                if (state_disappear)
                {
                    opacity -= 0;
                    if(isText)
                        Text.color = changeOpacity(Text.color, opacity);
                    else
                        Image.color = changeOpacity(Image.color, opacity);
                    
                }
            }
            else
            {
                moveX = disX * Time.deltaTime / time;
                moveY = disY * Time.deltaTime / time;
                disX -= moveX;
                disY -= moveY;
                time -= Time.deltaTime;
                gameObject.transform.Translate(moveX, moveY, 0);
                if (state_disappear)
                {
                    opacity -= opacity * Time.deltaTime / time;
                    if (isText)
                        Text.color = changeOpacity(Text.color, opacity);
                    else
                        Image.color = changeOpacity(Image.color, opacity);
                }
            }
        }
    }

    // Use this for initialization

    // Update is called once per frame

}
