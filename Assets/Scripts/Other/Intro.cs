using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Intro : MonoBehaviour
{
    public GameObject GL;
    public GameObject sc;

    private bool Zoom = true;

    // Start is called before the first frame update
    void Start()
    {
        sc.GetComponent<Scared>().SetCheckScared(false);
        GL.GetComponent<Light2D>().intensity = 0;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Zoom)
        {
            if (GL.GetComponent<Light2D>().intensity < 0.05f) { GL.GetComponent<Light2D>().intensity += 0.0004f; }
            else
            {
                sc.GetComponent<Scared>().SetCheckScared(true);
                Zoom = false;
            }
        }
    }
}