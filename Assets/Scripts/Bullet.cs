using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] GameObject bulletEffect;
    [SerializeField] private float speed = 10;
    [SerializeField] private float bulletRange;
    [SerializeField] private bool continuousDamage;
    [SerializeField] private bool poison;
    [SerializeField] private bool speedDown;
    [SerializeField] private int hit;
    [SerializeField] private float continuousTime;
    [SerializeField] private AudioClip[] hitSound;
    private Transform target;

    public float damage;
    public float posionDamage;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            hitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void hitTarget()
    {
        if(bulletRange > 0)
        {
            splashHit();
        }
        else
        {
            GameObject effect = Instantiate(bulletEffect, transform.position, transform.rotation);
            Damage(target, continuousDamage);
            Destroy(effect, 0.3f);
        }
        Destroy(gameObject);
    }

    void splashHit()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(bulletRange, bulletRange), 0);
        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "Enemy")
            {
                Damage(collider.transform, false);
                GameObject effect = Instantiate(bulletEffect, collider.transform.position, collider.transform.rotation);
                Destroy(effect, 0.3f);
            }
        }
    }

    void Damage(Transform enemy, bool isContinuousDamage)
    {
        int rand = Random.Range(0, 3);
        SoundManager.instance.playSound(hitSound[rand]);
        if (isContinuousDamage)
        {
            if (poison)
            {
                enemy.gameObject.SendMessage("Damage", damage);
                enemy.gameObject.SendMessage("PoisonDamage", posionDamage);
            }
            else enemy.gameObject.SendMessage("ContinuousDamage", damage);
        }
        else
        {
            if(speedDown) enemy.gameObject.SendMessage("speedDown");
            enemy.gameObject.SendMessage("Damage", damage);
        }
    }
}
