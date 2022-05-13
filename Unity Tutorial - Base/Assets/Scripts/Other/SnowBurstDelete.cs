using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBurstDelete : MonoBehaviour
{
    private void Awake()
    {
        if (this.name != "SnowBurst")
        {
            Destroy(this.gameObject, 5);
        }
    }
}
