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
    private PlayerController _playerController;
    private static readonly string ANIMATOR_PARAMETER = "IsScared";

    public float ScaredTime = 10f;
    public float ScaredCountDown = 10f;
    public bool timerIsRunning = false;

    public CinemachineVirtualCamera BunkCam;
    public CinemachineVirtualCamera BunkEl;
    public CinemachineVirtualCamera BunkTun;

    private GameObject CheckTrue;
    public GameObject Check1;
    public GameObject Check2;

    public bool check = true;
    public bool check2 = true;

    public GameObject Scared1;
    public GameObject Scared2;
    public GameObject Scared3;

    public GameObject Darkness;

    // Start is called before the first frame update
    void Start()
    {
        lightdetection = lt.GetComponent<LightDetection>();
        camaram = cm.GetComponent<CamaraManager>();

        

        timerIsRunning = true;
        ScaredCountDown = ScaredTime;    
        _playerController = GetComponent<PlayerController>();
        Speed = _playerController.MovingState._speed;

    }

    private void OnDestroy()
    {

        _playerController.MovingState._speed = Speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerIsRunning)
        {

            check = false;


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

        if (Time.timeScale == 0) return;

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
                Ortho = 7;
                GlobalL = 0.05f;
                break;

            case "Cam Bunker R2":
                Ortho = 4;
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

        bool isScared = lightdetection.LightValue < 0.25f;
        _playerController.Animator.SetBool(ANIMATOR_PARAMETER, isScared);
        _playerController.MovingState._speed = isScared ? Speed_scared : Speed;

        if (isScared)

        {

            if (check2 == true)
            {
                Scared1.GetComponent<Animator>().SetBool("Scared", true);
                Scared2.GetComponent<Animator>().SetBool("Scared", true);
                Scared3.GetComponent<Animator>().SetBool("Scared", true);

                check = false;
            }

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
                    GL.GetComponent<Light2D>().intensity -= 0.00003f;
                }




            }

            else
            {
                Invoke("Death", 0.5f);
                Darkness.GetComponent<Animator>().SetBool("Dark", true);
            }
        }
        else
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

            Scared1.GetComponent<Animator>().SetBool("Scared", false);
            Scared2.GetComponent<Animator>().SetBool("Scared", false);
            Scared3.GetComponent<Animator>().SetBool("Scared", false);
            check2 = true;

            ScaredCountDown = ScaredTime;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            LastCheck = collision.gameObject.transform.position;
            CheckTrue = collision.gameObject;
        }
    }

    public void Death()
    {
        if (camaram._currentCamera.name == "Cam Bunker")
        {
            if(CheckTrue == Check1)
            {
                camaram._currentCamera = BunkEl;
                BunkCam.enabled = false;
                BunkEl.enabled = true;
            }
            if (CheckTrue == Check2)
            {
                camaram._currentCamera = BunkEl;
                BunkCam.enabled = false;
                BunkEl.enabled = true;
                LastCheck = Check1.transform.position;
            }

        }

        Scared1.GetComponent<Animator>().SetBool("Scared", false);
        Scared2.GetComponent<Animator>().SetBool("Scared", false);
        Scared3.GetComponent<Animator>().SetBool("Scared", false);
        Darkness.GetComponent<Animator>().SetBool("Dark", false);
        check2 = true;
        check = true;
        this.transform.position = LastCheck;
        ScaredCountDown = ScaredTime;
        this.GetComponentInChildren<Light2D>().intensity = 0.17f;
        timerIsRunning = true;
    }
}
