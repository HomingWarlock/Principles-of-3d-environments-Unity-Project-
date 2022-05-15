using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEnterDock : MonoBehaviour
{
    private ShipMovement ship_Script;
    private GameObject input_Prompt;
    private Text input_TipText;

    private void Awake()
    {
        ship_Script = GameObject.Find("ShipGroup").GetComponent<ShipMovement>();
        input_Prompt = GameObject.Find("enter_Ship_Prompt");
        input_TipText = GameObject.Find("ship_TipText").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (ship_Script.inside_Ship && ship_Script.can_Dock)
        {
            input_TipText.text = "Dock Ship";
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "ShipGroup")
        {
            if (!ship_Script.is_Docked)
            {
                ship_Script.can_Dock = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.name == "ShipGroup")
        {
            if (!ship_Script.is_Docked)
            {
                ship_Script.can_Dock = false;
            }
        }
    }
}