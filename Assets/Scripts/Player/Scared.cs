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
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        lightdetection = lt.GetComponent<LightDetection>();
        camaram = cm.GetComponent<CamaraManager>();

        GL.GetComponent<Light2D>().intensity = 0.05f;

        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (OrthReady)
        {

            switch (camaram._currentCamera.name)
            {


                case "Cam Basement":
                    Ortho = 6;
                    break;

                case "Cam Bunker":
                    Ortho = 7;
                    break;

                case "Cam Tunel":
                    Ortho = 3;
                    break;

                case "Cam Bunker D":
                    Ortho = 6;
                    break;

                case "Cam Bunker R":
                    Ortho = 9;
                    break;

                default:

                    break;
            }

            camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Ortho;
        }


        bool isScared = lightdetection.LightValue < 0.25f;
        _playerController.Animator.SetBool("IsScared", isScared);
        _playerController.MovingState._speed = isScared ? 3 : 4.5f;

        if (isScared)
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

                this.GetComponentInChildren<Light2D>().intensity = 0.17f;
            }
        }
        else
        {
            if (this.GetComponentInChildren<Light2D>().intensity < 0.17)
            {
                this.GetComponentInChildren<Light2D>().intensity += 0.002f;

            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            LastCheck = collision.gameObject.transform.position;
        }
    }
}
