using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phone : MonoBehaviour
{
    public GameObject my_phone;
    public GameObject my_gallery;

    public GameObject pos1;
    public GameObject pos2;

    [SerializeField] bool phone_active = false;
    [SerializeField] bool gal_active = false;

    public KeyCode get_phone;
    public KeyCode open_phone;
    public KeyCode open_gal;

    //phone and gallery
    public Sprite[] phoneList;
    public Sprite[] gallery;

    //death pictures
    public Sprite Death1;
    public Sprite Death2;
    public Sprite Death3;
    public Sprite Death4;
    public Sprite Death5;



    private void Start()
    {
        my_phone.transform.position = pos1.transform.position;
        GetComponent<Image>().sprite = phoneList[0];
        my_gallery.GetComponent<Image>().enabled = false;
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
        }


        //open gallery
        if (Input.GetKeyDown(open_gal) && gal_active == true)
        {
            GetComponent<Image>().sprite = phoneList[2];
            my_gallery.GetComponent<Image>().enabled = true;
        }





    }



}
