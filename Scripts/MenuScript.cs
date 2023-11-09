using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject buttonPlay;
    public GameObject buttonAbout;
    public GameObject buttonBack;
    public GameObject textAbout;
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowAbout()
    {
        buttonPlay.SetActive(false);
        buttonAbout.SetActive(false);
        buttonBack.SetActive(true);
        textAbout.SetActive(true);
    }

    public void HideAbout()
    {
        buttonPlay.SetActive(true);
        buttonAbout.SetActive(true);
        buttonBack.SetActive(false);
        textAbout.SetActive(false);
    }

}
