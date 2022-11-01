using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManagersubway : MonoBehaviour
{
    public static gameManagersubway Instance;

    public float timer = 0;
    public float timer_up = 300;
    public static bool initiatedLoop = false;

    public Deathreset dr;

    public PlayerMovementFPS playermove;

    private string displayMinutes = "";
    private string  displaySeconds = "";

    public TMP_Text timer_UI;

    /*
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }*/

    private void Start()
    {
        //initialize
        if (playermove == null)
        {
            playermove = GameObject.Find("Player").GetComponent<PlayerMovementFPS>();
        }

        if (timer_UI == null)
        {
            timer_UI = GameObject.Find("Timer").GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        //convert timer to clock format
        if(timer/60 < 10)
        {
            displayMinutes = "0" + Mathf.FloorToInt(timer / 60).ToString();
        }
        else
        {
            displayMinutes = Mathf.FloorToInt(timer / 60).ToString();
        }

        if (timer % 60 < 10)
        {
            displaySeconds = "0" + Mathf.RoundToInt(timer%60).ToString();
        }
        else
        {
            displaySeconds = Mathf.RoundToInt(timer % 60).ToString();
        }

        //display clock

        timer_UI.text = "12:" + displayMinutes+ ":" +  displaySeconds;
        
        
        //timer reset

        if (initiatedLoop)
        {
            //show how you died
                //reset player
                //deathreset codes
                dr.resetPos();
                //timer = 0;
                initiatedLoop = false;
                //timer += Time.deltaTime;

        }
        else
        {
            //endScreen.SetActive(false);
            if (timer >= timer_up)
            {
                StartCoroutine(timeLoop());
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    public IEnumerator timeLoop()//initiate timeloop after a few  seconds
    {
        dr.predeath();
        yield return new WaitForSeconds(5);
        initiatedLoop = true;
        timer = 0;
    }
}
