using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.CinemachineFreeLook;

public class phone : MonoBehaviour
{
    public GameObject my_phone;
    public GameObject my_gallery;

    public GameObject pos1;
    public GameObject pos2;

    [SerializeField] bool phone_active = false;
    [SerializeField] bool gal_active = false;
    public bool gal_show = false;

    public KeyCode get_phone;
    public KeyCode open_phone;
    public KeyCode open_gal;

    public KeyCode gal_forward;
    public KeyCode gal_back;

    //phone and gallery
    public Sprite[] phoneList;
    //public Sprite[]gallery;
    int i = 0;

    public List<Sprite> gallery = new List<Sprite>();

    //death pictures
    public Sprite Death1;
    public Sprite Death2;
    public Sprite Death3;
    public Sprite Death4;
    public Sprite Death5;

    public GameObject GM;

    private void Start()
    {
        my_phone.transform.position = pos1.transform.position;
        GetComponent<Image>().sprite = phoneList[0];
        my_gallery.GetComponent<Image>().enabled = false;
        my_gallery.GetComponent<Image>().sprite = gallery[0];
        gal_active = false;
    }

    private void Update()
    {

        //get and hide phone
        if (Input.GetKeyDown(get_phone) && phone_active == false)
        {
            my_phone.transform.position = pos2.transform.position;
            phone_active = true;
        }else if(Input.GetKeyDown(get_phone) && phone_active == true)
        {
            my_phone.transform.position = pos1.transform.position;
            phone_active = false;
            gal_active = false;
            gal_show = false;
            my_gallery.GetComponent<Image>().enabled = false;
            GetComponent<Image>().sprite = phoneList[0];
        }

        //open and close phone
        if(Input.GetKeyDown(open_phone) && phone_active == true && gal_active == false)
        {
           GetComponent<Image>().sprite = phoneList[1];
            gal_active = true;
        }else if(Input.GetKeyDown(open_phone) && phone_active == true && gal_active == true)
        {
            GetComponent<Image>().sprite = phoneList[0];
            gal_active = false;
            gal_show = false;
            my_gallery.GetComponent<Image>().enabled = false;
        }


        //open gallery
        if (Input.GetKeyDown(open_gal) && gal_active == true)
        {
            GetComponent<Image>().sprite = phoneList[2];           
            gal_show = true;
            
        }

        //gallery code
        if(gal_show == true)
        {
            my_gallery.GetComponent<Image>().enabled = true;
            GalleryControls();
        }

        print(gallery.Count());
        print(i);


        IfIDie();
    }


     void GalleryControls()
    {
        

        if (Input.GetKeyDown(gal_forward) && i < gallery.Count())
        {           
            i++;
            my_gallery.GetComponent<Image>().sprite = gallery[i];
        
        }

        if (Input.GetKeyDown(gal_back) && i > 0)
        {
            i--;
            my_gallery.GetComponent<Image>().sprite = gallery[i];
        }

       
    }


    public void IfIDie()
    {
        if(GM.GetComponent<gameManagersubway>().vendingMachine_death == true)
        {
            if (gallery.Contains(Death1))
            {
            }
            else
            {
                gallery.Add(Death1);
            }

        }


        if (GM.GetComponent<gameManagersubway>().light_death == true)
        {
            if (gallery.Contains(Death2))
            {
            }
            else
            {
                gallery.Add(Death2);
            }
        }

        if (GM.GetComponent<gameManagersubway>().securityCam_death == true)
        {
            if (gallery.Contains(Death1))
            {
            }
            else
            {
                gallery.Add(Death3);
            }
        }

        if (GM.GetComponent<gameManagersubway>().electricity_death == true)
        {
            if (gallery.Contains(Death1))
            {
            }
            else
            {
                gallery.Add(Death4);
            }
        }

        if (GM.GetComponent<gameManagersubway>().killer_death == true)
        {
            if (gallery.Contains(Death5))
            {
            }
            else
            {
                gallery.Add(Death5);
            }
        }

    }

}
