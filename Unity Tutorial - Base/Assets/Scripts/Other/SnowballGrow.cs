using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballGrow : MonoBehaviour
{
    private float snow_ball_current;
    private float snow_ball_max;

    private void Awake()
    {
        snow_ball_current = 4;
        snow_ball_max = Random.Range(200, 600);
    }

    void OnCollisionStay(Collision col)
    {
        if (col.transform.name == "Doom Mountain")
        {
            if (snow_ball_current < snow_ball_max)
            {
                snow_ball_current += 3;
                this.transform.localScale += new Vector3(3, 3, 3);
            }
        }
    }
}
