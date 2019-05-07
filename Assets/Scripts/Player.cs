using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

   
    [SerializeField] private float range = 2;
    [SerializeField] private float attackRate = 1;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int price;
    [SerializeField] private int unitIndex;
    [SerializeField] private bool isDoubleAttack;
    [SerializeField] private GameObject attackRange;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip[] upgradeOrSell;

    public int upgradePrice;
    public Button upgradeButton;
    public Button sellButton;
    public Text upgradeText;
    public Text sellText;
    
    private Transform target;
    private float attackCountdown = 0;
    private Vector3 mousePos;
    private int sellPrice;
    private SpriteRenderer sprite;
    private int checkCount = 0;
    private Animator animator;

    UIManager ui;
    playerState state;
    SoundManager sound;

    void Start () {
        InvokeRepeating("UpdateTarget", 0, 1);
        ui = UIManager.instance;
        state = playerState.instance;
        sound = SoundManager.instance;
        sellPrice = price / 2;
        upgradePrice = 100 + state.playersUpgrade[unitIndex] * 10;
        upgradeButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update () {
        if (target == null) return;
        if (attackCountdown <= 0)
        {
            Attack();
            attackCountdown = 1 / attackRate;
        }
        attackCountdown -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (upgradeButton.gameObject.activeSelf == false) return;
        Vector3 scrennPos = Camera.main.WorldToScreenPoint(transform.position);
        upgradeButton.transform.position = new Vector3(scrennPos.x, scrennPos.y + 65, scrennPos.z);
        sellButton.transform.position = new Vector3(scrennPos.x, scrennPos.y - 65, scrennPos.z);
        upgradePrice = 100 + state.playersUpgrade[unitIndex] * 10;
        upgradeText.text = upgradePrice.ToString();
        sellText.text = sellPrice.ToString();
    }
    void Attack()
    {
        checkCount++;
        if (isDoubleAttack)
        {
            attackRate = 5;
            if(checkCount % 2 ==0)
            {
                attackRate = 1;
                checkCount = 0;
            }
        }
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.damage += state.playersUpgrade[unitIndex] * 5;
        bullet.posionDamage += state.playersUpgrade[unitIndex] * 5;

        if (bullet != null)
            bullet.Seek(target);
        sound.playSound(shoot);
    }

    void UpdateTarget()
    {
        //enemy 태크를 가진 오브젝트를 넣는다
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //가장 작은거리 , 일단 무한대로 설정
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //현재 위치에서 enemy의 거리
            float distnaceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distnaceToEnemy < shortestDistance)
            {
                shortestDistance = distnaceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
        SettingAnimation(target);
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) )
        {
            if (upgradeButton.gameObject.activeSelf == false)
            {
                if (state.isOnUpgradeButton) return;
                upgradeButton.gameObject.SetActive(true);
                sellButton.gameObject.SetActive(true);
                state.isOnUpgradeButton = true;
                sprite.color = new Color(1, 0.6f, 0, 1);
            }
            else if (upgradeButton.gameObject.activeSelf == true)
            {
                upgradeButton.gameObject.SetActive(false);
                sellButton.gameObject.SetActive(false);
                state.isOnUpgradeButton = false;
                sprite.color = Color.white;
            }
        }
    }

    public void Upgrade()
    {
        if (ui.userMoney < upgradePrice) return;
        state.playersUpgrade[unitIndex]++;
        ui.userMoney -= upgradePrice;
        upgradePrice = 100 + state.playersUpgrade[unitIndex] * 10;
        sound.playSound(upgradeOrSell[0]);
    }

    public void Sell()
    {
        ui.userMoney += sellPrice;
        state.isOnUpgradeButton = false;
        sound.playSound(upgradeOrSell[1]);
        Destroy(gameObject);
    }
    void SettingAnimation(Transform Enemy)
    {
        if (!Enemy)
        {
            StartCoroutine(animationSetIdle());
            return;
        }
        Vector3 dir = (Enemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(Enemy.transform.position.y - transform.position.y, Enemy.transform.position.x - transform.position.x) * 180 / Mathf.PI;
        if (angle > -45 && angle < 45)
        {
            sprite.flipX = false;
            animator.SetInteger("PlayerState", 3);
        }
        else if (angle >= 45 && angle < 135)
        {
            animator.SetInteger("PlayerState", 2);
        }
        else if (angle <= -45 && angle > -135)
        {
            animator.SetInteger("PlayerState", 1);
        }
        else
        {
            sprite.flipX = true;
            animator.SetInteger("PlayerState", 3);
        }
    }

    IEnumerator animationSetIdle()
    {
        animator.SetInteger("PlayerState", 0);
        yield return new WaitForSeconds(1.5f);
    }
}
