using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoScript : MonoBehaviour
{

    [SerializeField] GameObject spawnManager;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI pointsInfo;
    [SerializeField] TextMeshProUGUI lifesInfo;
    [SerializeField] TextMeshProUGUI immortalityInfo;
    [SerializeField] TextMeshProUGUI mainInfo;
    private SpawnManagerScript spawnManagerScript;
    private PlayerScript playerScript;
    bool isMainInfoShown = false;

    // Start is called before the first frame update
    void Start()
    {
        mainInfo.text = "";
        spawnManagerScript = spawnManager.GetComponent<SpawnManagerScript>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePointsInfo();
        UpdateLifesInfo();
        UpdateImmortalityInfo();
    }

    void UpdatePointsInfo()
    {
        int points = spawnManagerScript.SpawnCounter;
        pointsInfo.text = "Points: " + points;
    }

    void UpdateLifesInfo()
    {
        int lifes = playerScript.Lifes;
        lifesInfo.text = "Lifes: " + lifes;
    }
    void UpdateImmortalityInfo()
    {
        if (playerScript.IsImmortal)
        {
            immortalityInfo.text = "Immortality Test Mode";
        } else
        {
            immortalityInfo.text = "";
        }
    }

    public void ShowMainInfoWithText(string text)
    {
        if (!isMainInfoShown)
        {
            isMainInfoShown = true;
            mainInfo.text = text;
            StartCoroutine(HideInfoText());
            isMainInfoShown = false;
        }
    }

    IEnumerator HideInfoText()
    {
        yield return new WaitForSeconds(2);
        mainInfo.text = "";
    }
}
