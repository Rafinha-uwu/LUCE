using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class Scared : MonoBehaviour
{

    LightDetection lightdetection;
    public GameObject lt;

    CamaraManager camaram;
    public GameObject cm;

    private float Ortho;
    private bool OrthReady;

    public GameObject GL;

    public Vector2 LastCheck;

    // Start is called before the first frame update
    void Start()
    {
        lightdetection = lt.GetComponent<LightDetection>();
        camaram = cm.GetComponent<CamaraManager>();

        GL.GetComponent<Light2D>().intensity = 0.05f;


    }

    // Update is called once per frame
    void Update()
    {

        if (OrthReady)
        {

            switch (camaram._currentCamera.name)
            {


                case "Cam Basement":
                    Ortho = 5;
                    break;

                case "Cam Bunker":
                    Ortho = 7;
                    break;

                default:

                    break;
            }

            camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Ortho;
        }


        if (lightdetection.LightValue < 0.25f)
        {
            if (this.GetComponentInChildren<Light2D>().intensity > 0)
            {

                if (OrthReady)
                {
                    OrthReady = false;
                }

                this.GetComponentInChildren<Light2D>().intensity -= 0.0001f;

                if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 3)
                {
                    camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= 0.0005f;


                }

                if (GL.GetComponent<Light2D>().intensity > 0.01f)
                {
                    GL.GetComponent<Light2D>().intensity -= 0.00003f;
                }




            }

            else
            {

                Debug.Log("Dead");
                this.transform.position = LastCheck;

            }

        }
        else if (lightdetection.LightValue >= 0.25f)
        {
            if (this.GetComponentInChildren<Light2D>().intensity < 0.3)
            {
                this.GetComponentInChildren<Light2D>().intensity += 0.002f;


                if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < Ortho)
                {
                    camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 0.012f;
                }
                if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > Ortho)
                {
                    OrthReady = true;
                    
                }

                if (GL.GetComponent<Light2D>().intensity < 0.05f)
                {
                    GL.GetComponent<Light2D>().intensity += 0.001f;
                }

                if (GL.GetComponent<Light2D>().intensity > 0.05f)
                {
                    
                    GL.GetComponent<Light2D>().intensity = 0.05f;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            LastCheck = collision.gameObject.transform.position;
        }
    }
}