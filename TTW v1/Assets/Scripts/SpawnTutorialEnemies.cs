using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTutorialEnemies : MonoBehaviour
{
    public GameObject tutorialEnemy;
    public Transform tutorialEnemySpawner;
    public Transform tutorialEnemySpawner1;
    public Transform tutorialEnemySpawner2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(tutorialEnemy, tutorialEnemySpawner.position, tutorialEnemySpawner.rotation);
            Instantiate(tutorialEnemy, tutorialEnemySpawner1.position, tutorialEnemySpawner1.rotation);
            Instantiate(tutorialEnemy, tutorialEnemySpawner2.position, tutorialEnemySpawner2.rotation);
            gameObject.SetActive(false);
        }
    }
}
