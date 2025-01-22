using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.CinemachineOrbitalTransposer;

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

    public Transform Box10;
    private Vector3 XBox10;

    public Transform Box11;
    private Vector3 XBox11;

    public Transform Box12;
    private Vector3 XBox12;


    public Transform Hand;
    private Vector3 XHand;

    private PuzzleEye puzzle;
    public GameObject pp;

    private Hand hand;
    public GameObject mao;

    public GameObject L1;
    public GameObject L2;
    public GameObject L3;
    public GameObject L4;
    public GameObject L5;
    public GameObject L6;
    public GameObject L8;

    public GameObject TrapLight2;
    public GameObject TrapLight3;
    public GameObject TrapLight4;

    public GameObject Camera;

    [SerializeField] private Lever leverToControl1;
    [SerializeField] private Lever leverToControl2;
    [SerializeField] private Lever leverToControl3;


    // Start is called before the first frame update
    void Start()
    {
        camaram = cm.GetComponent<CamaraManager>();
        scared = sc.GetComponent<Scared>();
        hand = mao.GetComponent<Hand>();

        puzzle = pp.GetComponent<PuzzleEye>();

        XBaseBox = BaseBox.position;
        XBox1 = Box1.position;
        XBox2 = Box2.position;
        XBox3 = Box3.position;
        XBox4 = Box4.position;
        XBox5 = Box5.position;
        XBox7 = Box7.position;
        XBox8 = Box8.position;
        XBox9 = Box9.position;
        XKey = Key.position;

        XValve = Valve.position;

        XHand = Hand.position;

        XBox10 = Box10.position;
        XBox11 = Box11.position;
        XBox12 = Box12.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (camaram._currentCamera.name == "Cam Basement")
        {
            if (scared.check == true)
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
            XBox5 = Box5.position;
            XBox7 = Box7.position;
            XBox8 = Box8.position;

            XValve = Valve.position;

        }

        if (camaram._currentCamera.name == "Cam Bunker D2" || camaram._currentCamera.name == "Cam Elevator")
        {
            if (camaram._currentCamera.name == "Cam Bunker D2")
            {

                TrapLight.GetComponent<Animator>().SetBool("Reset", true);
                Invoke("HardReset", 1);

            }

            if (scared.check == true)
            {
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

        if (camaram._currentCamera.name == "Cam TP1")
        {
            if (scared.check == true)
            {
                puzzle.Phase = 1f;
            }

        }
        else
        {


        }

        if (camaram._currentCamera.name == "Cam Tunel 2")
        {
            if (scared.check == true)
            {
                Hand.position = XHand;
                hand.move = true;

                L1.gameObject.SetActive(true);
                L2.gameObject.SetActive(true);
                L3.gameObject.SetActive(true);
                L4.gameObject.SetActive(true);
                L5.gameObject.SetActive(true);
                L6.gameObject.SetActive(true);
                L8.gameObject.SetActive(true);

                TrapLight2.GetComponent<Animator>().SetBool("Open", false);
                TrapLight3.GetComponent<Animator>().SetBool("Open", false);
                TrapLight4.GetComponent<Animator>().SetBool("Open", false);

                TrapLight2.GetComponent<Animator>().SetBool("Reset", true);
                TrapLight3.GetComponent<Animator>().SetBool("Reset", true);
                TrapLight4.GetComponent<Animator>().SetBool("Reset", true);
                Invoke("HardReset", 1);

                Box10.position = XBox10;
                Box11.position = XBox11;
                Box12.position = XBox12;

                leverToControl1.SetLeverState(false);
                leverToControl2.SetLeverState(false);
                leverToControl3.SetLeverState(false);

                Transform parentTransform = Camera.transform.parent;


                if (parentTransform.name == "HoldPosition")
                {

                }
                else
                {
                    Camera.transform.position = new Vector3(253, -2.4f, 0);
                }

            }

        }
        else
        {



        }



    }

    private void HardReset()
    {
        TrapLight.GetComponent<Animator>().SetBool("Reset", false);
        TrapLight2.GetComponent<Animator>().SetBool("Reset", false);
        TrapLight3.GetComponent<Animator>().SetBool("Reset", false);
        TrapLight4.GetComponent<Animator>().SetBool("Reset", false);
    }
}
