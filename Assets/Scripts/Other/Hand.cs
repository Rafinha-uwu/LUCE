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

    private Scared scared;
    public GameObject boo;

    public GameObject Black2;

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

        if(other.name == "Flash")
        {

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


    // Update is called once per frame
    void Update()
    {

        if (start)
        {
            //this.transform.position = new Vector3(142, -2, 0);
            Sprite.SetActive(true);
            Black.SetActive(true);
            Touchi.SetActive(true);
            this.GetComponent<Animator>().Play("Start");
            Invoke("Alive", 10.4f);
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
    }
}
