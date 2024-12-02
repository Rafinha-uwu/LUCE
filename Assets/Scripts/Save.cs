using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    CamaraManager camaram;
    public GameObject cm;

    Scared scared;
    public GameObject sc;

    public GameObject Player;

    public Transform BaseBox;
    private Vector3 XBaseBox;

    private bool oncelight = true;
    public GameObject BLight1;
    public GameObject BLight2;
    public Transform Box1;
    private Vector3 XBox1;
    public Transform Box2;
    private Vector3 XBox2;

    public GameObject Random;

    public Transform Box3;
    private Vector3 XBox3;
    public Transform Box4;
    private Vector3 XBox4;
    public Transform Box5;
    private Vector3 XBox5;
    public Transform Box6;
    private Vector3 XBox6;
    public Transform Box7;
    private Vector3 XBox7;
    public Transform Box8;
    private Vector3 XBox8;

    public GameObject TrapLight;
    public Transform Valve;
    private Vector3 XValve;

    public Transform Key;
    private Vector3 XKey;
    public Transform Box9;
    private Vector3 XBox9;

    // Start is called before the first frame update
    void Start()
    {
        camaram = cm.GetComponent<CamaraManager>();
        scared = sc.GetComponent<Scared>();

        XBaseBox = BaseBox.position;
        XBox1 = Box1.position;
        XBox2 = Box2.position;
        XBox3 = Box3.position;
        XBox4 = Box4.position;
        XBox5 = Box5.position;
        XBox6 = Box6.position;
        XBox7 = Box7.position;
        XBox8 = Box8.position;

        XValve = Valve.position;


    }

    // Update is called once per frame
    void Update()
    {

        if (camaram._currentCamera.name == "Cam Basement") 
        { 
            if(scared.check == true)
            {
                BaseBox.position = XBaseBox;
            }
        }
        else
        {
            if (camaram._currentCamera.name == "Cam Elevator" || camaram._currentCamera.name == "Cam Tunel")
            {
                XBaseBox = BaseBox.position;
            }
        }

        if (camaram._currentCamera.name == "Cam Bunker" || camaram._currentCamera.name == "Cam Elevator" || camaram._currentCamera.name == "Cam Tunel") 
        {
            if (oncelight == true)
            {
                BLight1.GetComponent<LightEffects>().flicker = true;
                BLight2.GetComponent<LightEffects>().flicker = true;
                oncelight = false;
            }
            if (scared.check == true)
            {
                Box1.position = XBox1;
                Box2.position = XBox2;
            }
        }
        else
        {

                XBox1 = Box1.position;
                XBox2 = Box2.position;

        }


        if (camaram._currentCamera.name == "Cam Bunker D1" || camaram._currentCamera.name == "Cam Elevator")
        {

            if (scared.check == true)
            {
                Random.GetComponent<Code>().Randomizer();
                Box3.position = XBox3;
                Box4.position = XBox4;
                Box5.position = XBox5;
                Box6.position = XBox6;
                Box7.position = XBox7;
                Box8.position = XBox8;
                TrapLight.GetComponent<Animator>().SetBool("Reset", true);
                Invoke("HardReset", 1);
                Valve.position = XValve;

            }

        }
        else
        {

            XBox3 = Box3.position;
            XBox4 = Box4.position;
            XBox5 = Box5.position;
            XBox6 = Box6.position;
            XBox7 = Box7.position;
            XBox8 = Box8.position;

            XValve = Valve.position;

        }

        if (camaram._currentCamera.name == "Cam Bunker D2" || camaram._currentCamera.name == "Cam Elevator") 
        {

            if (scared.check == true)
            {
                TrapLight.GetComponent<Animator>().SetBool("Reset", true);
                Invoke("HardReset", 1);
                Box9.position = XBox9;
                Player.GetComponent<PlayerHoldItem>().ForceDrop();
                if (Key.gameObject != null) { Key.position = XKey; }


            }
        }
        else
        {
            XBox9 = Box9.position;
            if (Key.gameObject != null) { XKey = Key.position; }
       
        }

        if (camaram._currentCamera.name == "Cam Bunker R") 
        { 
        


        }
        else
        {



        }


    }

    private void HardReset()
    {
        TrapLight.GetComponent<Animator>().SetBool("Reset", false);
    }
}
