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

    // Start is called before the first frame update
    void Start()
    {

        Collider2D childCollider = Touchi.GetComponent<Collider2D>();

    }

    public void OnChildTriggerEnter(GameObject other)
    {
        if (other.CompareTag("Player")) 
        {
            
        }

        if(other.name == "Flash")
        {

        }


    }


    // Update is called once per frame
    void Update()
    {

        if (start)
        {
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
