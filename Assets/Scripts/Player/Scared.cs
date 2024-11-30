using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Cinemachine;
using UnityEngine.Rendering;
using JetBrains.Annotations;

public class Scared : MonoBehaviour
{

    LightDetection lightdetection;
    public GameObject lt;

    CamaraManager camaram;
    public GameObject cm;


    private float Ortho;
    private bool OrthReady;


    private float GlobalL;
    private bool GLReady;

    public GameObject GL;

    public float Speed;
    public float Speed_scared;

    public Vector2 LastCheck;

    public float ScaredTime = 10f;
    public float ScaredCountDown = 10f;
    public bool timerIsRunning = false;

    public CinemachineVirtualCamera BunkCam;

    // Start is called before the first frame update
    void Start()
    {
        lightdetection = lt.GetComponent<LightDetection>();
        camaram = cm.GetComponent<CamaraManager>();

        timerIsRunning = true;
        ScaredCountDown = ScaredTime;

        

        Speed = this.GetComponent<PlayerController>().MovingState._speed;
    }

    private void OnDestroy()
    {
        this.GetComponent<PlayerController>().MovingState._speed = Speed;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerIsRunning)
        {
            if (ScaredCountDown > 0)
            {
                ScaredCountDown -= Time.deltaTime;
            }
            else
            {
                ScaredCountDown = 0;
                timerIsRunning = false;
            }
        }


        switch (camaram._currentCamera.name)
        {


            case "Cam Basement":
                Ortho = 6;
                GlobalL = 0.05f;
                break;


            case "Cam Elevator":
                Ortho = 5;
                GlobalL = 0.05f;
                break;

            case "Cam Bunker":
                Ortho = 7;
                GlobalL = 0.05f;
                break;

            case "Cam Tunel":
                Ortho = 3;
                GlobalL = 0.05f;
                break;

            case "Cam Bunker D1":
                Ortho = 6;
                GlobalL = 0.05f;
                break;

            case "Cam Bunker D2":
                Ortho = 6;
                GlobalL = 0.002f;
                break;



            case "Cam Bunker R":
                Ortho = 9;
                GlobalL = 0.05f;
                break;

            default:

                break;
        }


        if (OrthReady)
        {
            camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Ortho;
        }


        if (GLReady)
        {
            GL.GetComponent<Light2D>().intensity = GlobalL;
        }


        if (lightdetection.LightValue < 0.25f)
        {
            if (ScaredCountDown > 0)
            {

                if (OrthReady)
                {
                    OrthReady = false;
                }

                this.GetComponentInChildren<Light2D>().intensity -= 0.00035f;

                if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 3)
                {
                    camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= 0.0015f;


                }


                if (GLReady)
                {
                    GLReady = false;
                }

                if (GL.GetComponent<Light2D>().intensity > 0.001f)
                {
                    GL.GetComponent<Light2D>().intensity -= 0.00013f;
                }




            }

            else
            {
                Debug.Log("Dead");
                Invoke("Death", 1f);
            }

            this.GetComponent<PlayerController>().MovingState._speed = Speed_scared;

        }
        else if (lightdetection.LightValue >= 0.25f)
        {
            if (this.GetComponentInChildren<Light2D>().intensity < 0.17)
            {
                this.GetComponentInChildren<Light2D>().intensity += 0.006f;

            }
            if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < Ortho)
            {
                camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 0.012f;
            }
            if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize >= Ortho)
            {
                OrthReady = true;

            }

            if (GL.GetComponent<Light2D>().intensity < GlobalL)
            {
                GL.GetComponent<Light2D>().intensity += 0.002f;
            }

            if (GL.GetComponent<Light2D>().intensity >= GlobalL)
            {

                GLReady = true;
            }


            ScaredCountDown = ScaredTime;
            this.GetComponent<PlayerController>().MovingState._speed = Speed;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            LastCheck = collision.gameObject.transform.position;
        }
    }

    public void Death()
    {

        this.transform.position = LastCheck;
        ScaredCountDown = ScaredTime;
        this.GetComponentInChildren<Light2D>().intensity = 0.17f;
        timerIsRunning = true;
    }
}
