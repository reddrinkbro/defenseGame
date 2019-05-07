using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    public int userLife;
    public int userMoney;
    public int currentEnemyNum;
    public int currentRound;

    [SerializeField] private Text round;
    [SerializeField] private Text enemyText;
    [SerializeField] private Text money;
    [SerializeField] private Text life;
    [SerializeField] private Text gameoverText;
    [SerializeField] GameObject gameOverObeject;
    [SerializeField] GameObject optionObject;
    [SerializeField] GameObject gameWinObject;
    [SerializeField] Slider[] volume;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip gameWinSound;
    public int costMoney;


    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
        Time.timeScale = 1;
        userLife = 10;
        userMoney = 15000;
        currentRound = 1;
    }

    public void MoneyDecrease(int money)
    {
        userMoney -= money; 
    }

    public void MoneyIncrease(int money)
    {
        userMoney += money;
    }
    public bool buyUnit(int cost)
    {
        if (userMoney < cost) return false;
        userMoney -= cost;
        return true;
    }

    public void SellUnit(int cost)
    {
        userMoney += cost;
    }

    void Update()
    {
        round.text = "Round " + currentRound.ToString();
        enemyText.text = currentEnemyNum.ToString();
        money.text = userMoney.ToString();
        life.text = userLife.ToString();
        if(userLife == 0)
        {
            Time.timeScale = 0;
            if (gameOverObeject.activeSelf == false) SoundManager.instance.bgmSoundChange(gameOverSound);
            gameOverObeject.SetActive(true);
            GameOver();
        }
        else if(currentRound >40)
        {
            GameWin();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            Time.timeScale *= 2;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Time.timeScale = 1;
        }
        if (optionObject.activeSelf == true)
        {
            musiceVolume();
        }
    }

    public void GameOver()
    {
        gameoverText.text = "YOU LIVE " + (currentRound - 1) + " ROUND!!";
    }

    public void clickRetryButton()
    {
        optionObject.SetActive(false);
        SoundManager.instance.playSound(click);
    }

    public void clickMainMenu()
    {
        SceneManager.LoadScene("mainTitleScene");
        SoundManager.instance.playSound(click);
    }

    public void showOption()
    {
        optionObject.SetActive(true);
    }
    public void musiceVolume()
    {
        SoundManager.instance.volumeControl(true, volume[0].value);
        SoundManager.instance.volumeControl(false, volume[1].value);
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        if (gameWinObject.activeSelf == false) SoundManager.instance.bgmSoundChange(gameWinSound);
        SoundManager.instance.loopOff();
        gameWinObject.SetActive(true);
    }
}
