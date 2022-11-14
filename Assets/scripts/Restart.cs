using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Restart : MonoBehaviour
{
    public VideoPlayer end;
    void Update()
    {
        end.loopPointReached += gobackMenu;
    }
    void gobackMenu(VideoPlayer vp)
    {
        Application.Quit();
        //SceneManager.LoadScene("menu");
    }
}
