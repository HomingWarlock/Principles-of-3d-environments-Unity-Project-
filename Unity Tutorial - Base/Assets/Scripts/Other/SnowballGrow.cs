using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballGrow : MonoBehaviour
{
    private float snow_ball_current;
    private float snow_ball_max;
    private ParticleSystem snow_burst;

    private void Awake()
    {
        snow_ball_current = 4;
        snow_ball_max = Random.Range(200, 600);
        snow_burst = GameObject.Find("SnowBurst").GetComponent<ParticleSystem>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.name == "ground")
        {
            ParticleSystem this_snow = Instantiate(snow_burst, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity) as ParticleSystem;
            Destroy(this.gameObject);
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.transform.name == "Mountain")
        {
            if (snow_ball_current < snow_ball_max)
            {
                snow_ball_current += 3;
                this.transform.localScale += new Vector3(3, 3, 3);
            }
        }
    }
}
