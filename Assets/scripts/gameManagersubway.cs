using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameManagersubway : MonoBehaviour
{
    public static gameManagersubway Instance;

    public float timer = 0;
    public float timer_up = 300;
    public bool initiatedLoop = false;

    public PlayerMovementFPS playermove;

    public GameObject endScreen;
    public TMP_Text timer_UI;

    public bool debug = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        /*endScreen = GameObject.Find("GameOverScreen");
        playermove = GameObject.Find("Player").GetComponent<PlayerMovementFPS>();
        timer_UI = GameObject.Find("Timer").GetComponent<TMP_Text>();*/
    }

    void Update()
    {
        timer_UI.text = Mathf.Round(timer).ToString();

        if (playermove == null)
        {
            playermove = GameObject.Find("Player").GetComponent<PlayerMovementFPS>();
        }

        if(endScreen == null)
        {
            endScreen = GameObject.Find("GameOverScreen");
        }

        if (timer_UI == null) 
        {
            timer_UI = GameObject.Find("Timer").GetComponent<TMP_Text>();
        }
        
        //timer 5 minutes reset

        if (initiatedLoop)
        {
            //show how you died
            if (Input.GetKeyDown(KeyCode.Space))
            {
                endScreen.SetActive(false);
                timer = 0;
                initiatedLoop = false;
                //turn on the playermovement
                playermove.enabled = true;
            }
            else
            {
                endScreen.SetActive(true);
                playermove.enabled = false;
            }

        }
        else
        {
            endScreen.SetActive(false);
            if (timer >= timer_up && !debug)
            {
                timeLoop();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    void timeLoop()
    {
        initiatedLoop = true;
        SceneManager.LoadScene("Subway", LoadSceneMode.Single);
    }
}
