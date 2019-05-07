using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float speed = 10;
    [SerializeField] private int enemyMoney;

    private Transform target;
    private int wavePointIndex = 0;
    private Animator animator;
    private SpriteRenderer sprite;
    private float currentHp;
    private Image hpBar;
    private float maxHp = 130;
    float tmpSpeed;
    UIManager ui;
    void Start()
    {
        target = Waypoints.points[0];
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        ui = UIManager.instance;
        hpBar = GetComponentInChildren<Image>();
        maxHp += ui.currentRound * 20;
        currentHp = maxHp;
        tmpSpeed = speed;
    }

    void Update()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
            ui.MoneyIncrease(enemyMoney);
            ui.currentEnemyNum--;
            return;
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //거리가 0.2보다 작아질때 웨이포인트를 변경
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        updateHpBar();
    }
    //다음 웨이 포인트로 변경
    void GetNextWaypoint()
    {
        //인덱스를 증가해서 다음 웨이포인트로 변경
        if(wavePointIndex >=Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            ui.currentEnemyNum--;
            ui.userLife--;
            return;
        }
        Transform beforeTarget = target;
        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
        setXAnimation(beforeTarget, target);
        setYAnimation(beforeTarget, target);
    }

    void setXAnimation(Transform before, Transform after)
    {
        if (after.position.x - before.position.x == 0) return;
        //오른쪽
        else if(after.position.x > before.position.x)
        {
            animator.SetInteger("EnemyState",0);
            sprite.flipX = false;
        }

        //왼쪽
        else if (after.position.x < before.position.x)
        {
            sprite.flipX = true;
            animator.SetInteger("EnemyState", 0);
        }
    }
    
    void setYAnimation(Transform before, Transform after)
    {
        if (after.position.y - before.position.y == 0) return;
        //위로
        else if (after.position.y > before.position.y)
        {
            animator.SetInteger("EnemyState", 2);
        }
        //아래로
        else if (after.position.y < before.position.y)
        {
            animator.SetInteger("EnemyState", 1);
        }
        
    }

    public void Damage(int damage)
    {
        currentHp -= damage;
    }

    IEnumerator ContinuousDamage(int damage)
    {
        for (int i = 0; i < 10; i++)
        {
            currentHp -= damage;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator PoisonDamage(int damage)
    {
        sprite.color = new Color(0.7f, 0.29f, 0.97f);
        for (int i = 0; i < 3; i++)
        {
            currentHp -= damage;
            yield return new WaitForSeconds(1);
        }
    }

    void updateHpBar()
    {
        hpBar.fillAmount = currentHp / maxHp;
    }

    IEnumerator speedDown()
    {
        while (tmpSpeed == speed)
        {
            speed /= 2;
            yield return new WaitForSeconds(1.5f);
        }
        speed = tmpSpeed;
    }
}
