using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject ship_object;
    private ShipMovement ship_Script;
    private GameObject environment;
    private AudioClip shoutingClip;
    private float speedDampTime = 0.01f;
    private float sensitivityX = 1.0f;
    private Animator anim;
    private HashIDs hash;
    private GameObject player_cam;
    private Transform campoint_player;
    private Transform campoint_ship;
    private Transform campoint_disaster;
    private bool cutscene_wait;
    private bool ship_Toggle_Delay;
    private Rigidbody ourBody;

    private void Awake()
    {
        ship_object = GameObject.Find("ShipGroup");
        ship_Script = ship_object.GetComponent<ShipMovement>();
        environment = GameObject.Find("Enviroment");
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 1f);
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        player_cam = GameObject.Find("Main_Camera");
        campoint_player = GameObject.Find("cam_point_playerview").transform;
        campoint_ship = GameObject.Find("cam_point_shipcontrol").transform;
        campoint_disaster = GameObject.Find("cam_point_disaster").transform;
        cutscene_wait = true;
        player_cam.transform.SetParent(environment.transform);
        player_cam.transform.localPosition = new Vector3(campoint_disaster.transform.localPosition.x, campoint_disaster.transform.localPosition.y, campoint_disaster.transform.localPosition.z);
        player_cam.transform.localRotation = Quaternion.Euler(campoint_disaster.transform.localRotation.eulerAngles.x, campoint_disaster.transform.localRotation.eulerAngles.y, campoint_disaster.transform.localRotation.eulerAngles.z);
        StartCoroutine(SwitchToGame());

        ship_Toggle_Delay = false;
        ourBody = this.GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (!cutscene_wait)
        {
            float v = Input.GetAxis("Vertical");
            bool sneak = Input.GetButton("Sneak");
            float turn = Input.GetAxis("Turn");
            Rotating(turn);
            MovementManagement(v, sneak);
        }
    }

    public void Update()
    {
        if (!cutscene_wait)
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
                        player_cam.transform.SetParent(ship_object.transform);
                        player_cam.transform.localPosition = new Vector3(campoint_ship.transform.localPosition.x, campoint_ship.transform.localPosition.y, campoint_ship.transform.localPosition.z);
                        player_cam.transform.localRotation = Quaternion.Euler(campoint_ship.transform.localRotation.eulerAngles.x, campoint_ship.transform.localRotation.eulerAngles.y, campoint_ship.transform.localRotation.eulerAngles.z);
                        anim.SetFloat(hash.speedFloat, 0);
                        this.transform.SetParent(GameObject.Find("ShipGroup").transform);
                        ship_Script.is_Docked = false;
                        ship_Script.ship_rb.velocity = Vector3.zero;
                        ship_Script.ship_rb.angularVelocity = Vector3.zero;
                        ourBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
                        ourBody.useGravity = false;
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
                        player_cam.transform.SetParent(ship_object.transform);
                        player_cam.transform.localPosition = new Vector3(campoint_ship.transform.localPosition.x, campoint_ship.transform.localPosition.y, campoint_ship.transform.localPosition.z);
                        player_cam.transform.localRotation = Quaternion.Euler(campoint_ship.transform.localRotation.eulerAngles.x, campoint_ship.transform.localRotation.eulerAngles.y, campoint_ship.transform.localRotation.eulerAngles.z);
                        anim.SetFloat(hash.speedFloat, 0);
                        this.transform.SetParent(GameObject.Find("ShipGroup").transform);
                        ship_Script.ship_rb.velocity = Vector3.zero;
                        ship_Script.ship_rb.angularVelocity = Vector3.zero;
                        ourBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
                        ourBody.useGravity = false;
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
                    player_cam.transform.SetParent(this.transform);
                    player_cam.transform.localPosition = new Vector3(campoint_player.transform.localPosition.x, campoint_player.transform.localPosition.y, campoint_player.transform.localPosition.z);
                    player_cam.transform.localRotation = Quaternion.Euler(campoint_player.transform.localRotation.eulerAngles.x, campoint_player.transform.localRotation.eulerAngles.y, campoint_player.transform.localRotation.eulerAngles.z);
                    this.transform.SetParent(null);
                    ship_Script.ship_rb.velocity = Vector3.zero;
                    ship_Script.ship_rb.angularVelocity = Vector3.zero;
                    ourBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    ourBody.useGravity = true;
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
                    player_cam.transform.SetParent(this.transform);
                    player_cam.transform.SetParent(this.transform);
                    player_cam.transform.localPosition = new Vector3(campoint_player.transform.localPosition.x, campoint_player.transform.localPosition.y, campoint_player.transform.localPosition.z);
                    player_cam.transform.localRotation = Quaternion.Euler(campoint_player.transform.localRotation.eulerAngles.x, campoint_player.transform.localRotation.eulerAngles.y, campoint_player.transform.localRotation.eulerAngles.z);
                    ship_object.transform.position = new Vector3(-51.92f, 5.07f, -70.71f);
                    this.transform.SetParent(null);
                    ship_Script.is_Docked = true;
                    ship_Script.ship_rb.velocity = Vector3.zero;
                    ship_Script.ship_rb.angularVelocity = Vector3.zero;
                    ourBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    ourBody.useGravity = true;
                }
            }

            if (ship_Script.inside_Ship && ship_Script.can_Lock)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    ship_Script.ship_rb.velocity = Vector3.zero;
                    ship_Script.ship_rb.angularVelocity = Vector3.zero;
                    ship_object.transform.rotation = Quaternion.Euler(0, 0, 0);
                    ship_object.transform.position = new Vector3(-168.3f, 29.5f, 1.5f);
                }
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

    private IEnumerator SwitchToGame()
    {
        //yield return new WaitForSeconds(15); // Cutscene Time
        yield return new WaitForSeconds(0); //Debug Cutscene Time
        cutscene_wait = false;
        player_cam.transform.SetParent(this.transform);
        player_cam.transform.localPosition = new Vector3(campoint_player.transform.localPosition.x, campoint_player.transform.localPosition.y, campoint_player.transform.localPosition.z);
        player_cam.transform.localRotation = Quaternion.Euler(campoint_player.transform.localRotation.eulerAngles.x, campoint_player.transform.localRotation.eulerAngles.y, campoint_player.transform.localRotation.eulerAngles.z);
    }
}