using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEnterCollide : MonoBehaviour
{
    private ShipMovement ship_Script;
    private GameObject input_Prompt;
    private Text input_TipText;

    private Rigidbody ourBody;

    private void Awake()
    {
        ship_Script = GameObject.Find("ShipGroup").GetComponent<ShipMovement>();
        input_Prompt = GameObject.Find("input_Prompt");
        input_TipText = GameObject.Find("input_TipText").GetComponent<Text>();
        input_Prompt.SetActive(false);

        ourBody = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (ship_Script.inside_Ship && !ship_Script.can_Dock)
        {
            input_TipText.text = "to Exit";
        }

        if (!ship_Script.inside_Ship)
        {
            input_TipText.text = "to Use";
        }
    }

    private void OnTriggerEnter(Collider col)
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

    private void OnTriggerExit(Collider col)
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
