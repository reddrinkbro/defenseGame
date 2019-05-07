using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerState : MonoBehaviour {
    public static playerState instance;
    public int[] playersUpgrade = new int[7];
    public bool isOnUpgradeButton;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            playersUpgrade[i] = 0;
        }
        isOnUpgradeButton = false;
    }
}
