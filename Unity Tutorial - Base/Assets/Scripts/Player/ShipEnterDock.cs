using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEnterDock : MonoBehaviour
{
    public ShipMovement ship_Script;
    public GameObject input_Prompt;
    public Text input_TipText;

    private void Awake()
    {
        ship_Script = GameObject.Find("ShipGroup").GetComponent<ShipMovement>();
        input_Prompt = GameObject.Find("input_Prompt");
        input_TipText = GameObject.Find("input_TipText").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (ship_Script.inside_Ship && ship_Script.can_Dock)
        {
            input_TipText.text = "Dock Ship";
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.name == "ShipGroup")
        {
            if (!ship_Script.is_Docked)
            {
                ship_Script.can_Dock = true;
            }
        }
    }

    public void OnTriggerExit(Collider col)
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