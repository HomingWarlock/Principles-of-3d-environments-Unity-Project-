using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public bool inside_Trigger;
    public bool inside_Ship;

    private Rigidbody ship_rb;

    private float speed;
    private float flight_speed;
    private float rotation_speed;
    private GameObject Box1;
    private GameObject Box2;

    public bool can_Dock;
    public bool is_Docked;

    private GameObject[] engine_steam;

    private void Awake()
    {
        inside_Trigger = false;
        inside_Ship = false;
        ship_rb = this.GetComponent<Rigidbody>();
        speed = 20f;
        flight_speed = 200f;
        rotation_speed = 1f;
        Box1 = GameObject.Find("Box1");
        Box2 = GameObject.Find("Box2");
        can_Dock = false;
        is_Docked = false;

        engine_steam = new GameObject[4];

        for (int e = 0; e < engine_steam.Length; e++)
        {
            engine_steam[e] = GameObject.Find("EngineSteam" + e);
        }
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

                if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                {
                    ship_rb.AddForce(transform.right * speed);
                }

                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                    {
                        ship_rb.AddForce(-transform.forward * speed);
                    }

                    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                    {
                        ship_rb.AddForce(transform.forward * speed);
                    }
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    ship_rb.AddForce(transform.up * flight_speed);

                    for (int e = 0; e < engine_steam.Length; e++)
                    {
                        engine_steam[e].SetActive(true);
                    }
                }
                else if (!Input.GetKey(KeyCode.UpArrow))
                {
                    for (int e = 0; e < engine_steam.Length; e++)
                    {
                        engine_steam[e].SetActive(false);
                    }
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    ship_rb.AddForce(-transform.up * flight_speed);
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