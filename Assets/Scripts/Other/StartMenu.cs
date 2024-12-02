using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;

public class StartMenu : MonoBehaviour
{

    public GameObject Menu;
    public GameObject Black;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NewGame()
    {
        Black.GetComponent<Animator>().SetBool("Dark", true);
        Menu.GetComponent<Animator>().SetBool("Start", true);
        Invoke("Load", 16);
    }
    public void Load()
    {

        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        Application.Quit();
    }

}


