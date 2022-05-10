using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public bool inside_Trigger;
    public bool inside_Ship;

    private Rigidbody ship_rb;

    public GameObject ship_Cam;
    private float speed;
    private float ascend_speed;
    private float descend_speed;
    private float rotation_speed;
    private GameObject Box1;
    private GameObject Box2;

    public bool can_Dock;
    public bool is_Docked;

    private void Awake()
    {
        inside_Trigger = false;
        inside_Ship = false;
        ship_rb = this.GetComponent<Rigidbody>();
        ship_Cam = GameObject.Find("ShipCam");
        ship_Cam.SetActive(false);
        speed = 20f;
        ascend_speed = 600f;
        descend_speed = 200f;
        rotation_speed = 1f;
        Box1 = GameObject.Find("Box1");
        Box2 = GameObject.Find("Box2");
        can_Dock = false;
        is_Docked = false;
    }

    private void Update()
    {
        if (!is_Docked)
        {
            if (inside_Ship)
            {
                Box1.SetActive(false);
                Box2.SetActive(false);

                if (Input.GetKey(KeyCode.W))
                {
                    ship_rb.AddForce(-transform.right * speed);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    ship_rb.AddForce(transform.right * speed);
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    ship_rb.AddForce(transform.up * ascend_speed);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    ship_rb.AddForce(-transform.up * descend_speed);
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    this.transform.Rotate(0, -45 * rotation_speed * Time.deltaTime, 0);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    this.transform.Rotate(0, 45 * rotation_speed * Time.deltaTime, 0);
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
}