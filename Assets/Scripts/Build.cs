using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Build : MonoBehaviour {

    public static Build instance;
    public GameObject[] selectPlayerPrefab;
    public Image[] progressBar;
    private GameObject playerToBuild;
    
    void Awake()
    {
        if(instance == null) instance = this;
    }
   
    public GameObject GetPlayerToBuild()
    {
        return playerToBuild;
    }

    public void SetPlayerToBuild(GameObject player)
    {
        playerToBuild = player;
    }
    
}
