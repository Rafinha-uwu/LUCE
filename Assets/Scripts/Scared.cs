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

    public GameObject GL;

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


        if (lightdetection.LightValue < 0.25f)
        {
            if (this.GetComponentInChildren<Light2D>().intensity > 0)
            {
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

            }

        }
        else if (lightdetection.LightValue >= 0.25f)
        {
            if (this.GetComponentInChildren<Light2D>().intensity < 0.3)
            {
                this.GetComponentInChildren<Light2D>().intensity += 0.002f;

                if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < 5)
                {
                    camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 0.01f;
                }
                if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 5)
                {
                    camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5;
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
}
