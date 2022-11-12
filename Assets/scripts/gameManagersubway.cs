using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public bool electricity_death = false;
    public bool killer_death = false;
    public bool securityCam_death = false;

    public bool canendgame = false;

    public int death_count = 0;

    public GameObject Player;

    public GameObject Friend;
    public GameObject Newspaper;
    public GameObject LooseLight;
    public GameObject LooseLight_description;
    public GameObject LooseLight_trigger;
    public GameObject VendingMachine;
    public GameObject ElectrifyToilet;
    public GameObject IceTea;

    public GameObject Ad;
    public GameObject SecurityCam;

    public GameObject Monologues;

    public Camera Player_cam;
    public Camera Light_cam;

    public bool Light_Fell = false;
    public int Light_fell_timer = 5;

    public int Cameras_investigated = 0;

    public bool dead = false; // important *****

    public bool hint_ad = false;
    public bool hint_securityCam = false;
    public bool hint_friendPhone = false;

    public GameObject finalChoice;

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
        finalChoice.SetActive(false);
        SceneReset(1);
    }

    void Update()
    {
        if (hint_ad && hint_friendPhone && hint_securityCam)
        {
            canendgame = true;
        }

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
            if (timer >= timer_up)
            {
                player_died();
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
            Light_cam.enabled = true;
            Player_cam.enabled = false;
            LooseLight.GetComponent<Animator>().SetBool("fall", true);
            Light_Fell = true;
            
            //activate light collider for five seconds
            LooseLight_trigger.SetActive(true);
        }

        if (timer >= Light_fell_timer + 5)
        {
            Light_cam.enabled = false;
            Player_cam.enabled = true;

            LooseLight_trigger.SetActive(false);
        }

        if(Cameras_investigated == 3)
        {
            //investigated all cameras
            hint_securityCam = true;
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
        Light_cam.enabled = false;
        Player_cam.enabled = true;
        IceTea.SetActive(false);
        LooseLight_trigger.SetActive(false);

        //reset light pos
        Light_Fell = false;
        LooseLight.GetComponent<Animator>().SetBool("fall", false);

        if (index == 0)//reset scene when initializing not used yet*
        {

        }

        //newspaper - killer

        SetDescription(killer_death, Newspaper);
        SetDescription(light_death, LooseLight_description);
        SetDescription(vendingMachine_death, VendingMachine);

        foreach (Transform child in Friend.transform)
        {
            child.gameObject.SetActive(false);
        }

        Friend.transform.GetChild(death_count).gameObject.SetActive(true);

        var mono = Monologues.transform.GetChild(death_count).gameObject.GetComponent<DialogueLoad>();
        //monologue 
        dm.LoadDialogue(mono);
    }

    public void RemoveAD(GameObject ad)
    {
        var ad_parent = ad.transform.parent;
        //reveal option to remove ad
        if (!hint_ad)
        {
            
            ad_parent.transform.GetChild(0).gameObject.SetActive(false);
            ad_parent.transform.GetChild(1).gameObject.SetActive(true);
            hint_ad = true;
        }
        else
        {
            Ad.SetActive(false);
            ad_parent.transform.GetChild(1).gameObject.SetActive(false);
            ad_parent.transform.GetChild(2).gameObject.SetActive(true);
        }
        
    }

    //function to quickly reset item description
    void SetDescription(bool death_trigger, GameObject item)
    {
        var set_description_num = death_trigger ? 1 : 0;
        foreach (Transform child in item.transform)
        {
            child.gameObject.SetActive(false);
        }
        item.transform.GetChild(set_description_num).gameObject.SetActive(true);
    }

    public void endGame(bool win)
    {
        if (win)
        {
            SceneManager.LoadScene("win_End", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("lose_End", LoadSceneMode.Single);

        }
    }
}
