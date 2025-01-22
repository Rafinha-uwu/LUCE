using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool start = false;

    public GameObject Sprite;
    public GameObject Black;
    public GameObject Touchi;

    public float speed;
    public bool move = false;
    public bool cool = false;

    private Scared scared;
    public GameObject boo;

    public GameObject Black2;

    public GameObject Box;
    public GameObject Block;
    public GameObject Block2;

    // Start is called before the first frame update
    void Start()
    {
        scared = boo.GetComponent<Scared>();

        Collider2D childCollider = Touchi.GetComponent<Collider2D>();

    }

    public void OnChildTriggerEnter(GameObject other)
    {
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(Death());
        }

        if (other.name == "Luz")
        {
            other.gameObject.SetActive(false);
        }

    }

    public IEnumerator Death()
    {
        move = false;
        this.GetComponent<Animator>().Play("Grab");
        PauseManager.Instance.PauseGame();
        yield return new WaitForSecondsRealtime(0.4f);
        Black2.GetComponent<Animator>().SetBool("Dark", true);
        yield return new WaitForSecondsRealtime(1f);
        move = true;
        scared.Death();
        PauseManager.Instance.ResumeGame();

    }

    public IEnumerator Flash()
    {
        move = false;
        cool = true;
        this.GetComponent<Animator>().Play("Flash");
        yield return new WaitForSecondsRealtime(3f);
        move = true;
        cool = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > 390)
        {
            move = false;
        }
        

        if (start)
        {

            Block2.SetActive(true);
            Sprite.SetActive(true);
            Black.SetActive(true);
            Touchi.SetActive(true);
            this.GetComponent<Animator>().Play("Start");
            Invoke("Alive", 10.4f);
            Invoke("Boxes", 9.1f);
            start = false;
        }

        if (move)
        {
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
            this.GetComponent<Animator>().Play("Move");
        }

    }

    public void Alive()
    {
        move = true;
        Block2.SetActive(false);
    }

    public void Boxes()
    {
        Box.GetComponent<Animator>().Play("Fall");
        Block.SetActive(false);
    }

    public void CallFlash()
    {
        StartCoroutine(Flash());
    }
}

