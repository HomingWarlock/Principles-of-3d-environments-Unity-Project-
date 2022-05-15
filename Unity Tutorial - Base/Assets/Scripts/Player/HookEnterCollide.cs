using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookEnterCollide : MonoBehaviour
{
    private ShipMovement ship_Script;
    private GameObject input_Prompt;
    private Text input_TipText;

    private void Awake()
    {
        ship_Script = GameObject.Find("ShipGroup").GetComponent<ShipMovement>();
        input_Prompt = GameObject.Find("enter_Hook_Prompt");
        input_TipText = GameObject.Find("hook_TipText").GetComponent<Text>();
        input_Prompt.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (ship_Script.inside_Ship_Hook)
        {
            input_TipText.text = "to Exit";
        }

        if (!ship_Script.inside_Ship_Hook)
        {
            input_TipText.text = "to Use";
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!ship_Script.inside_Ship_Hook)
            {
                input_Prompt.SetActive(true);
                ship_Script.inside_Hook_Trigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!ship_Script.inside_Ship_Hook && !ship_Script.inside_Trigger)
            {
                input_Prompt.SetActive(false);
                ship_Script.inside_Hook_Trigger = false;
            }
        }
    }
}