using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManagersubway : MonoBehaviour
{
    public static gameManagersubway Instance;

    public float timer = 0;
    public float timer_up = 300;
    public bool initiatedLoop = false;

    public Deathreset dr;
    public DialogueUI dm;

    public PlayerMovementFPS playermove;

    private string displayMinutes = "";
    private string  displaySeconds = "";

    public TMP_Text timer_UI;

    public bool died = false;
    public bool vendingMachine_death = false;
    public bool light_death = false;
    public bool securityCam_death = false;
    public bool electricity_death = false;
    public bool killer_death = false;

    public int death_count = 0;

    public GameObject Player;

    public GameObject Friend;
    public GameObject Newspaper;
    public GameObject LooseLight;
    public GameObject VendingMachine;
    public GameObject ElectrifyToilet;

    public GameObject Monologues;

    public bool Light_Fell = false;
    public int Light_fell_timer = 5;

    public bool dead = false; // important *****

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

        SceneReset(1);
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

        if (dead)
        {
            if(Input.GetKeyDown(KeyCode.R))
            dr.resetPos();
        }

        if (timer >= Light_fell_timer && !Light_Fell)
        {
            //drop light
            LooseLight.GetComponent<Animator>().SetBool("fall", true);
            Player.GetComponent<Animator>().enabled = true;
            Light_Fell = true; 
        }

        if (timer >= Light_fell_timer + 5)
        {
            // Avoid any reload.
            //Debug.Log("fini");
            Player.GetComponent<Animator>().enabled = false;
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
        //change dialogues according to the loop
        SceneReset(0);
        dead = true;
    }

    void SceneReset(int index)
    {
        Light_Fell = false;
        Player.GetComponent<Animator>().enabled = false;

        if (index == 0)
        {
            /*foreach (Transform child in Friend.transform)
            {
                child.gameObject.SetActive(false);
            }
            foreach (Transform child in Newspaper.transform)
            {
                child.gameObject.SetActive(false);
            }
            foreach (Transform child in VendingMachine.transform)
            {
                child.gameObject.SetActive(false);
            }*/
        }
        //newspaper - killer
        var newsnum = killer_death ? 1 : 0;
        foreach (Transform child in Newspaper.transform)
        {
            child.gameObject.SetActive(false);
        }
        Newspaper.transform.GetChild(newsnum).gameObject.SetActive(true);
        
        
        foreach (Transform child in Friend.transform)
        {
            child.gameObject.SetActive(false);
        }

        Friend.transform.GetChild(death_count).gameObject.SetActive(true);

        var mono = Monologues.transform.GetChild(death_count).gameObject.GetComponent<DialogueLoad>();
        //monologue 
        dm.LoadDialogue(mono);
    }


}
