using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class btn_start : MonoBehaviour
{
    public VideoPlayer phone;
    public VideoPlayer start;
    public GameObject menu;
    public GameObject startobj;

    void Start()
    {
        startobj.SetActive(false);
        menu.SetActive(false);
    }
    void Update()
    {
        phone.loopPointReached += enterMenu;
    }

    void enterMenu(VideoPlayer vp)
    {
        Debug.Log("Done");
        menu.SetActive(true);
    }

    public void startCG()
    {
        startobj.SetActive(true);
        start.Play();
        start.loopPointReached += LoadScene;
    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("Polished Sub");
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
