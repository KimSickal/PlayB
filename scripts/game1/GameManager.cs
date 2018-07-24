using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject cola;
    public GameObject badCola;
    public GameObject floatingText;
    public GameObject popText;
    public GameObject beltR;
    public GameObject beltL;

    public GameObject[] lives = new GameObject[5];

    public int magnifier = 10;

    public GameObject sp1L;
    public GameObject sp1R;
    public GameObject sp2L;
    public GameObject sp2R;
    public GameObject sp3L;
    public GameObject sp3R;
    public GameObject belt1;
    public GameObject belt2;
    public GameObject belt3;
    public GameObject floatingPoint;
    public GameObject backGroundLayer;
    public GameObject gameOverImage;
    public UnityEngine.UI.Text gameOverText;


    public bool sp1LOn = false;
    public bool sp2LOn = false;
    public bool sp3LOn = false;

    public UnityEngine.UI.Text LevelText;
    public UnityEngine.UI.Text ScoreText;

    public float pauseTimer = 5;

    public int level = 0;
    public int maxSpawn = 0;
    private float ranVal = 0;
    private bool goingRight = false;
    public int spawnedCola = 0;
    public int score = 0;
    public bool debug = false;

    private int counter = 3;

    public int life = 5;
    private int preLife = 5;


    // Use this for initialization
    void Start()
    {
        level = debug ? level : -1;
        maxSpawn = 2 * level + (int)Random.Range((int)2, (int)6);
        ranVal = debug ? ranVal : 0;
        spawnedCola = debug ? spawnedCola : 0;

        sp1LOn = debug ? sp1LOn : Random.Range(0f, 1f) >= 0.5;
        sp2LOn = debug ? sp2LOn : Random.Range(0f, 1f) >= 0.5;
        sp3LOn = debug ? sp3LOn : Random.Range(0f, 1f) >= 0.5;
        preLife = life;
        //ScoreText.text = "" + score;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        timer += Time.deltaTime;
        if(timer > period)
        {
            timer = 0;
            GameObject newCola = Instantiate(cola, spawnPoint.transform.position, Quaternion.identity);
            newCola.transform.SetParent(spawnPoint.transform);
            newCola.GetComponent<Cola>().Move(600);
        }
        */
        if (level == -1)
        {
            pauseTimer = pauseTimer - Time.deltaTime;
            if(pauseTimer < counter)
            {
                GameObject temp = Instantiate(popText, gameObject.transform.position, Quaternion.identity);
                temp.transform.SetParent(gameObject.transform);
                temp.GetComponent<AnimationHandler>().time = 0.5f;
                temp.GetComponent<AnimationHandler>().state_activated = true;
                temp.GetComponent<AnimationHandler>().state_destroy = true;
                temp.GetComponent<AnimationHandler>().state_disappear = true;
                temp.GetComponent<UnityEngine.UI.Text>().text = 
                    counter != 0 ? ("" + counter) : "Go!";
                counter--;
            }
            if(pauseTimer <= 0)
            {
                spawnedCola = 0;
                maxSpawn = 2 * level + (int)Random.Range((int)2, (int)6);
                sp1LOn = Random.Range(0f, 1f) >= 0.5;
                sp2LOn = Random.Range(0f, 1f) >= 0.5;
                sp3LOn = Random.Range(0f, 1f) >= 0.5;
                pauseTimer = 0.1f;
                level = 0;
                ranVal = Mathf.Max(Random.Range((float)(1.5f - (level * 0.10f)), (float)(4.2f - (level * 0.2f))), 0.3f);
            }

        }
        else if (pauseTimer > 0)
        {
            pauseTimer = pauseTimer - Time.deltaTime;
            if(pauseTimer <= 0)
            {
                pauseTimer = 0;
                MakeNewBelt(belt1, sp1LOn);
                MakeNewBelt(belt2, sp2LOn);
                MakeNewBelt(belt3, sp3LOn);
                score += level * 10;
                MakeNewFloatingText("레벨 보너스 +" + level * 10);
                level++;
                LevelText.text = "레벨 " + level;
                ranVal = Mathf.Max(Random.Range((float)(1.5f - (level * 0.10f)), (float)(4.2f - (level * 0.2f))), 0.3f);
            }
        }
        else
        {
            ranVal = Mathf.Max(ranVal - Time.deltaTime, 0);
            if (ranVal <= 0)
            {

                ranVal = Mathf.Max(Random.Range((float)(1.5f - (level * 0.10f)), (float)(4.2f - (level * 0.2f))), 0.3f);
                GameObject spawnPoint;
                float temp = Random.Range(0f, 1f);
                Debug.Log(temp);
                if (temp >= (2.0 / 3))
                {
                    spawnPoint = sp1LOn ? sp1L : sp1R;
                    goingRight = sp1LOn;
                }
                else if (temp >= (1.0 / 3))
                {
                    spawnPoint = sp2LOn ? sp2L : sp2R;
                    goingRight = sp2LOn;
                }
                else
                {
                    spawnPoint = sp3LOn ? sp3L : sp3R;
                    goingRight = sp3LOn;
                }
                temp = Random.Range(0f, 1f);
                GameObject newCola = Instantiate(((temp >= 0.3) ? cola : badCola),
                    spawnPoint.transform.position, Quaternion.identity);
                newCola.transform.SetParent(gameObject.transform);
                newCola.GetComponent<Cola>().Move(magnifier * (goingRight ? 1 : -1) * (13.0f + 0.2556f * level + 0.042f * level * level));
                //Debug.Log((goingRight ? 1 : -1) * (13.0f + 0.2556f * level + 0.042f * level * level));
                spawnedCola++;
                if (spawnedCola >= maxSpawn)
                {
                    spawnedCola = 0;
                    maxSpawn = 2 * level + (int)Random.Range((int)2, (int)6);
                    sp1LOn = Random.Range(0f, 1f) >= 0.5;
                    sp2LOn = Random.Range(0f, 1f) >= 0.5;
                    sp3LOn = Random.Range(0f, 1f) >= 0.5;
                    pauseTimer = Mathf.Min(5.0f, (750 / (magnifier * (goingRight ? 1 : -1) * (13.0f + 0.2556f * level + 0.042f * level * level))));
                }

            }
        }
        if (life == 0)
        {
            life = -1;
            gameOverImage.SetActive(true);
            gameOverText.text = ""+score;
            Time.timeScale = 0;

        }


    }
    public void MakeNewFloatingText(string text)
    {
        GameObject newText = Instantiate(floatingText, floatingPoint.transform.position, Quaternion.identity);
        newText.transform.SetParent(floatingPoint.transform);
        newText.GetComponent<AnimationHandler>().disY = 50;
        newText.GetComponent<AnimationHandler>().time = 0.5f;
        newText.GetComponent<AnimationHandler>().state_activated = true;
        newText.GetComponent<AnimationHandler>().state_destroy = true;
        newText.GetComponent<AnimationHandler>().state_disappear = true;
        newText.GetComponent<UnityEngine.UI.Text>().text = text;

    }
    public void MakeNewBelt(GameObject location, bool goingRight)
    {
        GameObject newBelt = Instantiate(goingRight ? beltR : beltL,
            location.transform.position, Quaternion.identity);
        newBelt.transform.Translate(new Vector3(goingRight ? -750 : 750, 0, 0));
        newBelt.transform.SetParent(backGroundLayer.transform);
        newBelt.GetComponent<AnimationHandler>().disX = (goingRight ? 1 : -1) * 750 / 2.0f;
        newBelt.GetComponent<AnimationHandler>().time = 1f;
        newBelt.GetComponent<AnimationHandler>().state_activated = true;
        newBelt.GetComponent<AnimationHandler>().state_destroy = true;
        newBelt.GetComponent<AnimationHandler>().state_disappear = true;
    }

    private void OnGUI()
    {
        if(level >= 1)
            ScoreText.text = "" + score;
        if(preLife > life)
        {
            for (int i = life; i < preLife; i++){
                lives[lives.Length - i - 1].GetComponent<AnimationHandler>().state_activated = true;
                lives[lives.Length - i - 1].GetComponent<AnimationHandler>().state_disappear = true;
                lives[lives.Length - i - 1].GetComponent<AnimationHandler>().time = 0.1f;
            }
        }
    }
}
