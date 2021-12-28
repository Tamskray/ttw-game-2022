using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPointPosChange : MonoBehaviour
{
    void Update()
    {
        if (PlayerController.facingRight)
        {
            transform.localPosition = new Vector2(0.374f, 0.002f);
        }
        else if (!PlayerController.facingRight)
        {
            transform.localPosition = new Vector2(0.374f - 0.75f, 0.002f);
        }
    }
}
