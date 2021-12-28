using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float cooldown;

    [HideInInspector] public bool isCoolDown;

    private Image shieldImage;
    private PlayerController player;

    void Start()
    {
        shieldImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        isCoolDown = true;
    }

    
    void Update()
    {
        if(isCoolDown)
        {
            shieldImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if(shieldImage.fillAmount <= 0)
            {
                shieldImage.fillAmount = 1;
                isCoolDown = false;
                player.shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTimer()
    {
        shieldImage.fillAmount = 1;
    }

    public void ReduceTimer(int damage)
    {
        shieldImage.fillAmount += damage / 7f;
    }
}
