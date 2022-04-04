using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public bool inside_Trigger;
    public bool inside_Ship;

    public GameObject ship_Cam;
    public float speed;
    public GameObject Box1;
    public GameObject Box2;

    public void Awake()
    {
        ship_Cam = GameObject.Find("ShipCam");
        ship_Cam.SetActive(false);
        speed = 20f;
        Box1 = GameObject.Find("Box1");
        Box2 = GameObject.Find("Box2");
    }

    public void Update()
    {
        if (inside_Ship)
        {
            Box1.SetActive(false);
            Box2.SetActive(false);
            
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(Input.GetAxis("Vertical") * speed * Time.deltaTime, 0, 0));
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(Input.GetAxis("Vertical") * speed * Time.deltaTime, 0, 0));
            }
        }
        else if (!inside_Ship)
        {
            Box1.SetActive(true);
            Box2.SetActive(true);
        }

        Box1.transform.Rotate(0, 45 * Time.deltaTime, 0);
        Box2.transform.Rotate(0, -45 * Time.deltaTime, 0);
    }
}