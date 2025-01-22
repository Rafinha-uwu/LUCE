using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static Unity.Collections.AllocatorManager;

public class PuzzleEye : MonoBehaviour
{

    // References to the colliders
    public Collider2D collider1;
    public Collider2D collider2;
    public Collider2D collider3;

    public float Phase = 0;

    public Light2D L1;
    public Light2D L11;

    public Light2D L2;
    public Light2D L22;

    public Light2D L3;
    public Light2D L33;

    public GameObject Doors;

    public GameObject Eye1;
    public GameObject Eye2;

    private Color NormalColor;

    private void Start()
    {
        // Ensure all colliders are trigger colliders
        collider1.isTrigger = true;
        collider2.isTrigger = true;
        collider3.isTrigger = true;

        NormalColor = L1.GetComponent<Light2D>().color;


    }

    private void Update()
    {
        if(Phase == 1)
        {
            L1.GetComponent<Light2D>().color = NormalColor;
            L11.GetComponent<Light2D>().color = NormalColor;
            L2.GetComponent<Light2D>().color = NormalColor;
            L22.GetComponent<Light2D>().color = NormalColor;
            L3.GetComponent<Light2D>().color = NormalColor;
            L33.GetComponent<Light2D>().color = NormalColor;
            Invoke("FOff1", 0);
            Invoke("FOn2", 0);
            Invoke("FOff3", 0);
            Doors.GetComponent<Animator>().Play("Close");
            Eye1.GetComponent<Animator>().Play("Start");
            Eye2.GetComponent<Animator>().Play("Start");
            Invoke("FOff2",5);
            Invoke("FOn1", 5);
            Invoke("EyeL", 5f);
            Phase = 1.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check which collider was triggered
        if (collision.IsTouching(collider1))
        {
            HandleCollider1Trigger(collision);
        }
        if (collision.IsTouching(collider2))
        {
            HandleCollider2Trigger(collision);
        }
        if (collision.IsTouching(collider3))
        {
            HandleCollider3Trigger(collision);
        }

    }

    private void HandleCollider1Trigger(Collider2D collision)
    {
        switch (Phase)
        {
            case 1.1f:
                Invoke("FOff1", 1);
                Invoke("FOn3", 1);
                Invoke("EyeR", 1f);
                Phase = 1.2f;
                break;

            case 2.1f:
                Invoke("FOn1", 0);
                Invoke("FOff1", 2);
                Invoke("EyeR", 0);
                Phase = 2.2f;
                break;

            case 2.3f:
                Invoke("FOn1", 0);
                Invoke("FOff1", 1);
                Invoke("EyeD", 0);
                Phase = 2.4f;
                break;

            case 3.1f:
                Invoke("FOff1", 0);
                Phase = 3.2f;
                break;

            case 3.7f:
                Invoke("FOff1", 0);
                Phase = 3.8f;
                break;

        }

    }

    private void HandleCollider2Trigger(Collider2D collision)
    {
        switch (Phase)
        {
            case 1.3f:
                Invoke("FOff2", 1);
                Invoke("FOn1", 1);
                Invoke("EyeL", 1f);
                Invoke("FOff1", 2);
                Invoke("FOn3", 2);
                Invoke("EyeR", 2f);
                Phase = 1.4f;
                break;

            case 1.5f:
                Invoke("FOff2", 3);
                L1.GetComponent<Light2D>().color = Color.red;
                L11.GetComponent<Light2D>().color = Color.red;
                L2.GetComponent<Light2D>().color = Color.red;
                L22.GetComponent<Light2D>().color = Color.red;
                L3.GetComponent<Light2D>().color = Color.red;
                L33.GetComponent<Light2D>().color = Color.red;
                Invoke("EyeL", 0);
                Phase = 2.1f;
                break;



            case 2.4f:
                Invoke("FOn2", 0);
                Invoke("FOff2", 1);
                Invoke("EyeR", 0);
                Phase = 2.5f;
                break;



            case 3.2f:
                Invoke("FOff2", 0);
                Phase = 3.3f;
                break;

            case 3.4f:
                Invoke("FOff2", 0);
                Phase = 3.5f;
                break;

            case 3.6f:
                Invoke("FOff2", 0);
                Phase = 3.7f;
                break;

            case 3.8f:
                Invoke("FOn1", 0f);
                Invoke("FOn2", 0f);
                Invoke("FOn3", 0f);
                L1.GetComponent<Light2D>().color = Color.green;
                L11.GetComponent<Light2D>().color = Color.green;
                L2.GetComponent<Light2D>().color = Color.green;
                L22.GetComponent<Light2D>().color = Color.green;
                L3.GetComponent<Light2D>().color = Color.green;
                L33.GetComponent<Light2D>().color = Color.green;

                Eye1.GetComponent<Animator>().Play("Up");
                Eye2.GetComponent<Animator>().Play("Up");
                Invoke("DoorUp", 1.5f);

                Phase = 4f;
                break;

        }
    }

    private void HandleCollider3Trigger(Collider2D collision)
    {
        switch (Phase)
        {
            case 1.2f:
                Invoke("FOff3", 1);
                Invoke("FOn2", 1);
                Invoke("EyeD", 1f);
                Phase = 1.3f;
                break;

            case 1.4f:
                Invoke("FOff3", 1);
                Invoke("FOn2", 1);
                Invoke("EyeD", 1f);
                Phase = 1.5f;
                break;


            case 2.2f:
                Invoke("FOn3", 0);
                Invoke("FOff3", 1);
                Invoke("EyeL", 0);
                Phase = 2.3f;
                break;
            case 2.5f:
                Invoke("FOn3", 0);
                Invoke("FOff3", 3);
                

                Invoke("FOn1", 3.5f);
                Invoke("EyeL", 3.5f);
                Invoke("FOff1", 4f);
              
                Invoke("FOn2", 4.5f);
                Invoke("EyeD", 4.5f);
                Invoke("FOff2", 5f);

                Invoke("FOn3", 5.5f);
                Invoke("EyeR", 5.5f);
                Invoke("FOff3", 6f);

                Invoke("EyeW", 6.5f);

                Phase = 3.1f;
                break;

            case 3.3f:
                Invoke("FOn3", 0);
                Invoke("EyeR", 0f);
                Invoke("FOff3", 2);

                Invoke("FOn2", 2.5f);
                Invoke("EyeD", 2.5f);
                Invoke("FOff2", 3f);

                Invoke("FOn3", 3.5f);
                Invoke("EyeR", 3.5f);
                Invoke("FOff3", 4f);

                Invoke("FOn2", 4.5f);
                Invoke("EyeD", 4.5f);
                Invoke("FOff2", 5f);

                Invoke("FOn1", 5.5f);
                Invoke("EyeL", 5.5f);
                Invoke("FOff1", 6f);

                Invoke("FOn2", 6.5f);
                Invoke("EyeD", 6.5f);
                Invoke("FOff2", 7f);

                Invoke("EyeW", 7.5f);
                Phase = 3.4f;
                break;

            case 3.5f:
                Invoke("FOff3", 0);
                Phase = 3.6f;
                break;
        }
    }

    public void DoorUp()
    {
        Doors.GetComponent<Animator>().Play("Open");
    }

    public void EyeW()
    {
        Eye1.GetComponent<Animator>().Play("Wiggle");
        Eye2.GetComponent<Animator>().Play("Wiggle");
    }
    public void EyeL()
    {
        Eye1.GetComponent<Animator>().Play("Left");
        Eye2.GetComponent<Animator>().Play("Right");
    }

    public void EyeD()
    {
        Eye1.GetComponent<Animator>().Play("Down");
        Eye2.GetComponent<Animator>().Play("Down");
    }

    public void EyeR()
    {
        Eye1.GetComponent<Animator>().Play("Right");
        Eye2.GetComponent<Animator>().Play("Left");
    }

    public void FOn1()
    {
        StartCoroutine(On1());
    }

    public void FOn2()
    {
        StartCoroutine(On2());
    }

    public void FOn3()
    {
        StartCoroutine(On3());
    }

    public void FOff1()
    {
        StartCoroutine(Off1());
    }

    public void FOff2()
    {
        StartCoroutine(Off2());
    }

    public void FOff3()
    {
        StartCoroutine(Off3());
    }

    public IEnumerator On1()
    {
        L1.GetComponent<Light2D>().intensity = 0.5f;
        L11.GetComponent<Light2D>().intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.5f);
        L1.GetComponent<Light2D>().intensity = 2;
        L11.GetComponent<Light2D>().intensity = 1f;

    }

    public IEnumerator On2()
    {
        L2.GetComponent<Light2D>().intensity = 0.5f;
        L22.GetComponent<Light2D>().intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.5f);
        L2.GetComponent<Light2D>().intensity = 2;
        L22.GetComponent<Light2D>().intensity = 1f;

    }

    public IEnumerator On3()
    {
        L3.GetComponent<Light2D>().intensity = 0.5f;
        L33.GetComponent<Light2D>().intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.5f);
        L3.GetComponent<Light2D>().intensity = 2;
        L33.GetComponent<Light2D>().intensity = 1f;

    }

    public IEnumerator Off1()
    {
        L1.GetComponent<Light2D>().intensity = 0.5f;
        L11.GetComponent<Light2D>().intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.1f);
        L1.GetComponent<Light2D>().intensity = 2;
        L11.GetComponent<Light2D>().intensity = 1f;
        yield return new WaitForSecondsRealtime(0.5f);
        L1.GetComponent<Light2D>().intensity = 0;
        L11.GetComponent<Light2D>().intensity = 0f;

    }

    public IEnumerator Off2()
    {
        L2.GetComponent<Light2D>().intensity = 0.5f;
        L22.GetComponent<Light2D>().intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.1f);
        L2.GetComponent<Light2D>().intensity = 2;
        L22.GetComponent<Light2D>().intensity = 1f;
        yield return new WaitForSecondsRealtime(0.5f);
        L2.GetComponent<Light2D>().intensity = 0;
        L22.GetComponent<Light2D>().intensity = 0f;

    }
    public IEnumerator Off3()
    {
        L3.GetComponent<Light2D>().intensity = 0.5f;
        L33.GetComponent<Light2D>().intensity = 0.5f;
        yield return new WaitForSecondsRealtime(0.1f);
        L3.GetComponent<Light2D>().intensity = 2;
        L33.GetComponent<Light2D>().intensity = 1f;
        yield return new WaitForSecondsRealtime(0.5f);
        L3.GetComponent<Light2D>().intensity = 0;
        L33.GetComponent<Light2D>().intensity = 0f;

    }



}