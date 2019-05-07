using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveSpawn : MonoBehaviour {

    public Transform[] enemyPrefab;

    public Transform spawnPoint;
    [SerializeField] private Text spawnText;
    private float timeBetweenWaves = 1000;
    private float countdown = 5;
    private int waveNumber = 1;
    private int enemyNumber = 10;
    private bool isStart = false;
    UIManager ui;

    void Start()
    {
        ui = UIManager.instance;
    }
    void Update()
    {
        //카운트 다운이 0이면 소환
        if(countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        //시간 마다 카운트 다운 줄어듬
        else countdown -= Time.deltaTime;
        if (isStart && ui.currentEnemyNum == 0)
        {
            ui.currentRound++;
            setRoundEnemy();
        }
        if (ui.currentEnemyNum == 0) isStart = false;
        spawnTimeText();
    }

    //소환하는것은 수정이 필요
    IEnumerator SpawnWave()
    {
        if (waveNumber == 0) yield break;
        for (int i = 0; i < enemyNumber; i++)
        {
            SpawnEnemy();   
            yield return new WaitForSeconds(1);
        }
        waveNumber--;
    }

    void SpawnEnemy()
    {
        //20라운드 미만
        if (ui.currentRound < 20)
        {
            int selectEnemy = (ui.currentRound - 1) % 4;
            Instantiate(enemyPrefab[selectEnemy], spawnPoint.position, spawnPoint.rotation);
        }
        //20라운드 이상
        else
        {
            int selectEnemy = ui.currentRound % 2;
            if (waveNumber == 2)
            {
                Instantiate(enemyPrefab[selectEnemy * 2], spawnPoint.position, spawnPoint.rotation);
            }
            else if(waveNumber == 1)
            {
                Instantiate(enemyPrefab[selectEnemy * 2 + 1], spawnPoint.position, spawnPoint.rotation);
            }
        }
        isStart = true;
        ui.currentEnemyNum++;
    }

    void setRoundEnemy()
    {
        if (ui.currentRound < 20)
        {
            waveNumber = 1;
            int roundEnemy = ui.currentRound / 5;
            enemyNumber = 10 + roundEnemy * 5;
            countdown = 5;
        }
        else
        {
            waveNumber = 2;
            enemyNumber = 20;
            countdown = 5;
        }
        spawnText.gameObject.SetActive(true);
    }

    void spawnTimeText()
    {
        spawnText.text = "몬스터 등장까지 " + ((int)countdown + 1) + "초";
        if (countdown < 0) spawnText.gameObject.SetActive(false);
    }
}
