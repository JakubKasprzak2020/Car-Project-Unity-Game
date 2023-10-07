using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("Button Play").GetComponentInChildren<Text>().text = "Play";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene("With Graphic");
        SceneManager.LoadScene("Intro");
    }
}
