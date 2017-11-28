using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    // Machine Gun Variables. 
    public float machineGunWaitTime = .2f;
    bool machineGunActive = true;
    public GameObject machineGunBullet;
    public Transform machineGunSpawnPoint;
    // Machine gun bullet variables
    GameObject machinegunBulletInstance;
    private Rigidbody bulletRB;
    public float bullVel = 75000f;
    // Lerp Variables
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        // Lerp Set up 
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }


    // Update is called once per frame
    void Update ()
    {
        // Input for when the LEFT mouse button is held down
        if (Input.GetMouseButton(0))
        {
            if (machineGunActive == true)
            {
                StartCoroutine(MachineGunFire());
            }
        }

        // Input for when the RIGHT mouse button is clicked
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Click");
        }

        // Input for when the MIDDLE mouse button is clicked
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Middle Click");
        }

    }

    IEnumerator MachineGunFire ()
    {
        machineGunActive = false;
        machinegunBulletInstance = Instantiate(machineGunBullet,machineGunSpawnPoint.transform.position, Quaternion.identity);
        bulletRB = machinegunBulletInstance.GetComponent<Rigidbody>();
        bulletRB.AddForce(transform.forward * Random.Range((bullVel -100.0f), (bullVel + 100.0f)));
        //Lerp Things
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        yield return new WaitForSeconds(machineGunWaitTime);
        machineGunActive = true;
    }


}
