using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundReaction : MonoBehaviour
{
    private GameObject snow_pile;
    private bool spawn_once;

    private void Awake()
    {
        snow_pile = GameObject.Find("BuriedSnow");
        snow_pile.SetActive(false);
        spawn_once = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.name == "snowball" && !spawn_once)
        {
            spawn_once = true;
            StartCoroutine(SpawnDelay());
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2);
        snow_pile.SetActive(true);
    }
}
