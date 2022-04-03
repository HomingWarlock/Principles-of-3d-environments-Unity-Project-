using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ShipMovement ship_Script;
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;

    private Animator anim;
    private HashIDs hash;

    public GameObject player_Cam;

    private void Awake()
    {
        ship_Script = GameObject.Find("RescueShip_Pfb").GetComponent<ShipMovement>();
        ship_Script.inside_Trigger = false;
        ship_Script.inside_Ship = false;
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 1f);
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();

        player_Cam = GameObject.Find("PlayerCam");
    }

    public void FixedUpdate()
    {
        if (!ship_Script.inside_Ship)
        {
            float v = Input.GetAxis("Vertical");
            bool sneak = Input.GetButton("Sneak");
            float turn = Input.GetAxis("Turn");
            Rotating(turn);
            MovementManagement(v, sneak);

            if (ship_Script.inside_Trigger)
            {
                if (Input.GetKey(KeyCode.F))
                {
                    ship_Script.inside_Ship = true;
                    ship_Script.inside_Trigger = false;
                    ship_Script.ship_Cam.SetActive(true);
                    player_Cam.SetActive(false);
                }
            }
        }

        if (ship_Script.inside_Ship)
        {
            if (Input.GetAxis("Vertical") > 0.5f)
            {
                transform.Translate(new Vector3(Input.GetAxis("Vertical") * ship_Script.speed * Time.deltaTime, 0, 0));
            }
        }
    }

    public void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout);
    }

    void MovementManagement(float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        if (vertical > 0)
        {
            anim.SetFloat(hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
        }
        else
        {
            anim.SetFloat(hash.speedFloat, 0);
        }
    }

    void Rotating(float mouseXInput)
    {
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        if (mouseXInput != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);

            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }

    void AudioManagement (bool shout)
    {
        if (anim.GetCurrentAnimatorStateInfo (0).IsName("Walk"))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().pitch = 0.27f;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }

        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}