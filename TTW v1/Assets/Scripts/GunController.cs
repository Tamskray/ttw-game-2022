using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GunType gunType;

    SpriteRenderer sprite;
    private PlayerController player;

    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    private float rotZ;

    public enum GunType {Default, Enemy};

    private float timeBtwShots;
    public float startTimeBtwShots;

    // ---joystick
    private Vector3 difference;
    public Joystick joystick;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        sprite = GetComponent<SpriteRenderer>();

        if(player.controlType == PlayerController.ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
        }
    }

    
    void Update()
    {
        if(gunType == GunType.Default)
        {
            if (PlayerController.facingRight)
            {
                sprite.flipX = false;
            }
            else if (!PlayerController.facingRight)
            {
                sprite.flipX = true;
            }

            //
            if (player.controlType == PlayerController.ControlType.PC)
            {
                Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            }
            else if (player.controlType == PlayerController.ControlType.Android && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
            {
                //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            }
        }
        else if(gunType == GunType.Enemy)
        {
            Vector3 difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }

        
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0) && player.controlType == PlayerController.ControlType.PC || gunType == GunType.Enemy)
            {
                Shoot();
            }
            else if(player.controlType == PlayerController.ControlType.Android)
            {
                if(joystick.Horizontal != 0 || joystick.Vertical != 0)
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
        //Instantiate(bullet, shotPoint.position, transform.rotation);
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        timeBtwShots = startTimeBtwShots;
    }
}
