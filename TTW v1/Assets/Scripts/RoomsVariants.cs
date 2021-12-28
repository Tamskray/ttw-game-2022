using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsVariants : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    public GameObject key;
    public GameObject gun;
    public GameObject portal;
    public GameObject portalDoor;

    [HideInInspector] public List<GameObject> rooms;

    private void Start()
    {
        StartCoroutine(RandomSpawner());
    }

    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(5f);
        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
        int rand = Random.Range(0, rooms.Count - 2);

        Instantiate(key, rooms[rand].transform.position, Quaternion.identity);
        Instantiate(gun, rooms[rooms.Count - 4].transform.position, Quaternion.identity);
        Instantiate(portal, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        Instantiate(portalDoor, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        //lastRoom.door.SetActive(true);
        //lastRoom.DestroyWalls();
    }
}
