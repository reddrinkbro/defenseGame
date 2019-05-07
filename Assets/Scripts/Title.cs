using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Title : MonoBehaviour {

    [SerializeField] private AudioClip click;

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
        SoundManager.instance.playSound(click);
    }

    public void GameEnd()
    {
        Application.Quit();
    }
}
