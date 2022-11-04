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

    public bool died = false;
    public bool vendingMachine_death = false;
    public bool light_death = false;
    public bool securityCam_death = false;
    public bool electricity_death = false;
    public int death_count = 0;

    public GameObject Friend;

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

        foreach (Transform child in Friend.transform)
        {
            child.gameObject.SetActive(false);
        }

        Friend.transform.GetChild(death_count).gameObject.SetActive(true);
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

        if (Mathf.RoundToInt(timer % 60) < 10)
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
            player_died();
            initiatedLoop = false;
        }
        else
        {
            //endScreen.SetActive(false);
            if (timer >= timer_up)
            {
                player_died();
                //StartCoroutine(timeLoop());
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

    }

    void player_died()
    {
        //reset player
        //deathreset codes
        dr.predeath();
        death_count += 1;
        died = true;
        //set the state of dialogue w friend
        if (death_count <=6)
        {
            Friend.transform.GetChild(death_count - 1).gameObject.SetActive(false);
            Friend.transform.GetChild(death_count).gameObject.SetActive(true);
        }
        timer = 0;
        dr.resetPos();
    }

    public IEnumerator timeLoop()//initiate timeloop after a few  seconds
    {
        dr.predeath();
        yield return new WaitForSeconds(2);
        initiatedLoop = true;
    }
}
