using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballGrow : MonoBehaviour
{
    void OnCollisionStay(Collision col)
    {
        if (col.transform.name == "Doom Mountain")
        {
            this.transform.localScale += new Vector3(3, 3, 3);
        }
    }
}
