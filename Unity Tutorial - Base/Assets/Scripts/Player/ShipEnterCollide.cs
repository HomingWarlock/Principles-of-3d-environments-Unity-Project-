using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEnterCollide : MonoBehaviour
{
    public ShipMovement ship_Script;
    public GameObject input_Prompt;

    private void Awake()
    {
        ship_Script = GameObject.Find("RescueShip_Pfb").GetComponent<ShipMovement>();
        input_Prompt = GameObject.Find("input_Prompt");
        input_Prompt.SetActive(false);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!ship_Script.inside_Ship)
            {
                input_Prompt.SetActive(true);
                ship_Script.inside_Trigger = true;
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!ship_Script.inside_Ship)
            {
                input_Prompt.SetActive(false);
                ship_Script.inside_Trigger = false;
            }
        }
    }
}
