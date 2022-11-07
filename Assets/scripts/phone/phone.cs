using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phone : MonoBehaviour
{
    public GameObject my_phone;

    public GameObject pos1;
    public GameObject pos2;

    [SerializeField] bool phone_active = false;

    public KeyCode get_phone;
    public KeyCode open_phone;

    //gallery
    public Sprite[] galleryList;
    

    private void Start()
    {
        my_phone.transform.position = pos1.transform.position;
        GetComponent<Image>().sprite = galleryList[0];

    }

    private void Update()
    {
        if (Input.GetKeyDown(get_phone) && phone_active == false)
        {
            my_phone.transform.position = pos2.transform.position;
            phone_active = true;
        }else if(Input.GetKeyDown(get_phone) && phone_active == true)
        {
            my_phone.transform.position = pos1.transform.position;
            phone_active = false;
            GetComponent<Image>().sprite = galleryList[0];
        }

        if(Input.GetKeyDown(open_phone) && phone_active == true)
        {
           GetComponent<Image>().sprite = galleryList[1];
        }


    }



}
