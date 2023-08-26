﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoScript : MonoBehaviour
{

    [SerializeField] GameObject spawnManager;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI pointsInfo;
    [SerializeField] TextMeshProUGUI immortalityInfo;
    private SpawnManagerScript spawnManagerScript;
    private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        spawnManagerScript = spawnManager.GetComponent<SpawnManagerScript>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePointsInfo();
        UpdateImmortalityInfo();
    }

    void UpdatePointsInfo()
    {
        int points = spawnManagerScript.SpawnCounter;
        pointsInfo.text = "Points: " + points;
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
}
