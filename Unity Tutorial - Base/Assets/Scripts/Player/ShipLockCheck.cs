using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLockCheck : MonoBehaviour
{
    private ShipMovement ship_Script;
    private GameObject input_Prompt;

    private void Awake()
    {
        ship_Script = GameObject.Find("ShipGroup").GetComponent<ShipMovement>();
        input_Prompt = GameObject.Find("lock_Ship_Prompt");
        input_Prompt.SetActive(false);
        ship_Script.can_Lock = false;
    }

    private void Update()
    {
        if (!ship_Script.inside_Ship)
        {
            input_Prompt.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.name == "ShipGroup")
        {
            if (ship_Script.inside_Ship)
            {
                input_Prompt.SetActive(true);
                ship_Script.can_Lock = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.name == "ShipGroup")
        {
            if (col.name == "ShipGroup")
            {
                if (ship_Script.inside_Ship)
                {
                    input_Prompt.SetActive(false);
                    ship_Script.can_Lock = false;
                }
            }
        }
    }
}
