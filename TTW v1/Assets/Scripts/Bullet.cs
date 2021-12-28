using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;

    public LayerMask whatIsSolid;
    public GameObject bulletEffect;

    [SerializeField] bool enemyBullet;

    //PlayerController player;

    private void Start()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }

            if (hitInfo.collider.CompareTag("EnemyBasic"))
            {
                hitInfo.collider.GetComponent<EnemyBasic>().TakeDamage(damage);
            }

            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                hitInfo.collider.GetComponent<PlayerController>().ChangeHealth(-damage);
                //hitInfo.collider.GetComponent<PlayerController>().TakePlayerDamage(damage);
            }

            DestroyBullet();
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    public void DestroyBullet()
    {
        Instantiate(bulletEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
