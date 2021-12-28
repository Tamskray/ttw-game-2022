using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject floatingDamage;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    //[HideInInspector] public bool playerNotInRoom;
    //private bool stopped;

    public int health;
    public float speed;
    private PlayerController player;
    public GameObject bulletEffect;

    public int damage;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;

    private Animator anim;
    private AddRoom room;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        room = GetComponentInParent<AddRoom>();
        //
        normalSpeed = speed;
    }

    void Update()
    {
        if(stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            //Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (room.enemies != null)
                room.enemies.Remove(gameObject);
        }

        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);


    }

    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
        Vector2 damagePos = new Vector2(transform.position.x, transform.position.y + 2.75f);
        //Instantiate(floatingDamage, damagePos, Quaternion.identity);
        //floatingDamage.GetComponentInChildren<FloatingDamage>().damage = damage;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("enemyAttack");
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void OnEnemyAttack()
    {

        Instantiate(bulletEffect, player.transform.position, Quaternion.identity);
        //player.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
        player.ChangeHealth(-damage);
    }
}
