using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    private Player player;
    private Vector3 difference;
    public Joystick joystick;
    private float rotZ;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform shotPoint;
    public Animator camAnim;

    public int rotationOffset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


        if (player.controlType == Player.ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (player.controlType == Player.ControlType.PC)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else if (player.controlType == Player.ControlType.Android && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
        {
            //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0) && player.controlType == Player.ControlType.PC)
            {
                Shoot();
            }
            else if (player.controlType == Player.ControlType.Android)
            {
                if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                {
                    Shoot();
                }
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        camAnim.SetTrigger("shake");
        Instantiate(projectile, shotPoint.position, transform.rotation);
        timeBtwShots = startTimeBtwShots;
    }
}
