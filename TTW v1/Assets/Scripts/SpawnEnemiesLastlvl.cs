using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesLastlvl : MonoBehaviour
{
    public int whichSpawn;

    public GameObject tutorialEnemy;
    public GameObject tutorialEnemywithGun;
    public Transform tutorialEnemySpawner;
    public Transform tutorialEnemySpawner1;
    public Transform tutorialEnemySpawner2;
    public Transform tutorialEnemySpawner3;
    public Transform tutorialEnemySpawner4;
    public Transform tutorialEnemySpawner5;
    public Transform tutorialEnemySpawner6;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (whichSpawn == 1)
            {
                Instantiate(tutorialEnemy, tutorialEnemySpawner.position, tutorialEnemySpawner.rotation);
                Instantiate(tutorialEnemy, tutorialEnemySpawner1.position, tutorialEnemySpawner1.rotation);
                Instantiate(tutorialEnemy, tutorialEnemySpawner2.position, tutorialEnemySpawner2.rotation);
                Instantiate(tutorialEnemy, tutorialEnemySpawner3.position, tutorialEnemySpawner3.rotation);
                Instantiate(tutorialEnemywithGun, tutorialEnemySpawner4.position, tutorialEnemySpawner4.rotation);
                Instantiate(tutorialEnemywithGun, tutorialEnemySpawner5.position, tutorialEnemySpawner5.rotation);
                Instantiate(tutorialEnemywithGun, tutorialEnemySpawner6.position, tutorialEnemySpawner6.rotation);
                gameObject.SetActive(false);
            }
        }
    }
}
