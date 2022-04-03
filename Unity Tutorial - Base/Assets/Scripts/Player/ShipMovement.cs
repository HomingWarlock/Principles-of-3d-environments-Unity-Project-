using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public bool inside_Trigger;
    public bool inside_Ship;

    public GameObject ship_Cam;
    public float speed;

    public void Awake()
    {
        ship_Cam = GameObject.Find("ShipCam");
        ship_Cam.SetActive(false);
        speed = 20f;
    }

    public void FixedUpdate()
    {
        if (inside_Ship)
        {
            if (Input.GetAxis("Vertical") > 0.5f)
            {
                transform.Translate(new Vector3(Input.GetAxis("Vertical") * speed * Time.deltaTime, 0, 0));
            }
        }
    }
}