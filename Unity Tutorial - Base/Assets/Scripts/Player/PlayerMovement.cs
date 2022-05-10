using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject ship_object;
    public ShipMovement ship_Script;
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    private Animator anim;
    private HashIDs hash;
    public GameObject player_Cam;
    public bool ship_Toggle_Delay;
    public Rigidbody ourBody;

    private void Awake()
    {
        ship_object = GameObject.Find("ShipGroup");
        ship_Script = ship_object.GetComponent<ShipMovement>();
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 1f);
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        player_Cam = GameObject.Find("PlayerCam");
        ship_Toggle_Delay = false;
        ourBody = this.GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        float turn = Input.GetAxis("Turn");
        Rotating(turn);
        MovementManagement(v, sneak);
    }

    public void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout);

        if (!ship_Script.inside_Ship && !ship_Script.is_Docked)
        {
            if (ship_Script.inside_Trigger)
            {
                if (Input.GetKeyDown(KeyCode.F) && !ship_Toggle_Delay)
                {
                    ship_Toggle_Delay = true;
                    StartCoroutine(ShipToggleTime(0f));
                    ship_Script.inside_Ship = true;
                    ship_Script.inside_Trigger = false;
                    ship_Script.ship_Cam.SetActive(true);
                    player_Cam.SetActive(false);
                    anim.SetFloat(hash.speedFloat, 0);
                    this.transform.SetParent(GameObject.Find("ShipGroup").transform);
                    ship_Script.is_Docked = false;
                    ourBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
                }
            }
        }


        if (!ship_Script.inside_Ship && ship_Script.is_Docked)
        {
            if (ship_Script.inside_Trigger)
            {
                if (Input.GetKeyDown(KeyCode.F) && !ship_Toggle_Delay)
                {
                    ship_Toggle_Delay = true;
                    StartCoroutine(ShipToggleTime(0f));
                    ship_Script.inside_Ship = true;
                    ship_Script.inside_Trigger = false;
                    ship_Script.ship_Cam.SetActive(true);
                    player_Cam.SetActive(false);
                    anim.SetFloat(hash.speedFloat, 0);
                    this.transform.SetParent(GameObject.Find("ShipGroup").transform);
                    ourBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
                }
            }
        }

        if (ship_Script.inside_Ship && !ship_Script.can_Dock)
        {
            if (Input.GetKeyDown(KeyCode.F) && !ship_Toggle_Delay)
            {
                ship_Toggle_Delay = true;
                StartCoroutine(ShipToggleTime(0f));
                ship_Script.inside_Ship = false;
                ship_Script.inside_Trigger = true;
                player_Cam.SetActive(true);
                ship_Script.ship_Cam.SetActive(false);
                this.transform.SetParent(null);
                ourBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
        }

        if (ship_Script.inside_Ship && ship_Script.can_Dock)
        {
            if (Input.GetKeyDown(KeyCode.F) && !ship_Toggle_Delay)
            {
                ship_Toggle_Delay = true;
                StartCoroutine(ShipToggleTime(0f));
                ship_Script.inside_Ship = false;
                ship_Script.inside_Trigger = true;
                player_Cam.SetActive(true);
                ship_Script.ship_Cam.SetActive(false);
                ship_object.transform.position = new Vector3(-51.92f, 5.07f, -70.71f);
                this.transform.SetParent(null);
                ship_Script.is_Docked = true;
                ourBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }

    void MovementManagement(float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        if (vertical > 0 && !ship_Script.inside_Ship)
        {
            anim.SetFloat(hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
        }
        else if (vertical <= 0 && !ship_Script.inside_Ship)
        {
            anim.SetFloat(hash.speedFloat, 0);
        }
    }

    void Rotating(float mouseXInput)
    {
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        if (mouseXInput != 0 && !ship_Script.inside_Ship)
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

    private IEnumerator ShipToggleTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ship_Toggle_Delay = false;
    }
}