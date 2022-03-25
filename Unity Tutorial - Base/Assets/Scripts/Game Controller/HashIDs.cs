using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int walkState;
    public int shoutState;
    public int dyingState;
    public int speedFloat;
    public int sneakingBool;
    public int shoutingBool;
    public int deadBool;

    private void Awake()
    {
        walkState = Animator.StringToHash("Walk");
        shoutState = Animator.StringToHash("Shouting.Shout");
        dyingState = Animator.StringToHash("Base Layer.Dying");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking");
        shoutingBool = Animator.StringToHash("Shouting");
        deadBool = Animator.StringToHash("Dead");
    }
}
