using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;

    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    private RoomsVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        // !spawned
        if(spawned == false)
        {
            if(direction == Direction.Top)
            {
                rand = Random.Range(0, variants.topRooms.Length);
                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Bottom)
            {
                rand = Random.Range(0, variants.bottomRooms.Length);
                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, variants.rightRooms.Length);
                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, variants.leftRooms.Length);
                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
        //can be Stay not Enter
        //just spawned
        if(other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned == true)
        {
            Destroy(gameObject);
        }
    }
}
