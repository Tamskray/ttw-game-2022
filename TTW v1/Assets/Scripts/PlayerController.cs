using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    

    // `ll joystick
    [Header("Controls")]
    public Joystick joystick; //----------------------
    public ControlType controlType; //----------------------
    [SerializeField] float moveSpeed;
    public enum ControlType { PC, Android } //----------------------


    Animator anim;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    public static bool facingRight = true;
    private bool keyButtonPushed;

    [Header("Health")]
    public float health;
    public Text healthDisplay;
    public GameObject potionEffect;

    [Header("Shield")]
    public GameObject shield;
    public Shield shieldTimer;
    public GameObject shieldEffect;

    [Header("Weapon")]
    public List<GameObject> unlockedWeapons;
    public GameObject[] allWeapons;
    public Image weaponIcone;

    [Header("Key")]
    public GameObject keyIcon;
    public GameObject wallEffect;

    [Header("Tips")]
    public Text movementTip;
    public Text buffsTip;
    public float timerStart = 10;
    private bool messageShown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        if(controlType == ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(messageShown)
            timerStart -= Time.deltaTime;

        if (timerStart < 0)
        {
            movementTip.gameObject.SetActive(false);
            buffsTip.gameObject.SetActive(false);
            messageShown = false;
            timerStart = 3;
        }

        //------------------------
        //moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(controlType == ControlType.PC)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if(controlType == ControlType.Android)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        moveVelocity = moveInput.normalized * moveSpeed;

        if (!facingRight && moveInput.x > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput.x < 0)
        {
            Flip();
        }

        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            facingRight = true;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnKeyButtonDown();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        anim.SetBool("isMoving", (Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0));
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Potion"))
        {
            ChangeHealth(5);
            Instantiate(potionEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Shield"))
        {
            if(!shield.activeInHierarchy)
            {
                shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isCoolDown = true;
                Instantiate(shieldEffect, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
            else
            {
                shieldTimer.ResetTimer();
                Instantiate(shieldEffect, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
        }
        else if(other.CompareTag("Weapon"))
        {
            for (int i = 0; i < allWeapons.Length; i++)
            {
                if(other.name == allWeapons[i].name)
                {
                    unlockedWeapons.Add(allWeapons[i]);
                }
            }
            SwitchWeapon();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Tips"))
        {
            if(timerStart > 0)
            {
                movementTip.gameObject.SetActive(true);
                messageShown = true;
            }
        }
        else if (other.CompareTag("Tip2"))
        {
            if (timerStart > 0)
            {
                buffsTip.gameObject.SetActive(true);
                messageShown = true;
            }
        }
        else if (other.CompareTag("Key"))
        {
            keyIcon.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    public void OnKeyButtonDown()
    {
        keyButtonPushed = !keyButtonPushed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Door") && keyButtonPushed && keyIcon.activeInHierarchy)
        {
            Instantiate(wallEffect, other.transform.position, Quaternion.identity);
            keyIcon.SetActive(false);
            other.gameObject.SetActive(false);
            keyButtonPushed = false;
        }
    }

    public void ChangeHealth(int healthValue)
    {
        if(!shield.activeInHierarchy || shield.activeInHierarchy && healthValue > 0)
        {
            health += healthValue;
            healthDisplay.text = "HP: " + health;
        }
        else if(shield.activeInHierarchy && healthValue < 0)
        {
            shieldTimer.ReduceTimer(healthValue);
        }
    }

    public void SwitchWeapon()
    {
        for(int i = 0; i < unlockedWeapons.Count; i++)
        {
            if(unlockedWeapons[i].activeInHierarchy)
            {
                unlockedWeapons[i].SetActive(false);
                if (i != 0)
                {
                    unlockedWeapons[i - 1].SetActive(true);
                    weaponIcone.sprite = unlockedWeapons[i - 1].GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    unlockedWeapons[unlockedWeapons.Count - 1].SetActive(true);
                    weaponIcone.sprite = unlockedWeapons[unlockedWeapons.Count - 1].GetComponent<SpriteRenderer>().sprite;
                }
                weaponIcone.SetNativeSize();
                break;
            }
        }
    }
}
