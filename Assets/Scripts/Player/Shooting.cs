using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    // Machine Gun Variables. 
    public float machineGunWaitTime = .2f;
    bool machineGunActive = true;

    // Machine gun bullet variables
    public GameObject machineGunBullet;
    public Transform machineGunSpawnPoint;
    GameObject machinegunBulletInstance;
    private Rigidbody bulletRB;
    public float bullVel = 75000f;
    public float randomBulletDistance = 5000f;

    //Single Shot Variables
    public GameObject singleShotBullet;
    public Transform singleShotGunSpawnPoint;

    // Lerp Variables
    public Transform startMarkerHeavy;
    public Transform endMarkerHeavy;
    public Transform startMarkerLight;
    public Transform endMarkerLight;
    public float lerpSpeed = 1.0F;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        // Lerp Set up 
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarkerLight.position, endMarkerLight.position);
        journeyLength = Vector3.Distance(startMarkerHeavy.position, endMarkerHeavy.position);

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
            StartCoroutine(singleShotGunActive());
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
        bulletRB.AddForce(transform.forward * Random.Range(bullVel - randomBulletDistance, bullVel + randomBulletDistance));
        //Lerp Things
        float distCovered = (Time.time - startTime) * lerpSpeed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarkerLight.position, endMarkerLight.position, fracJourney);
        yield return new WaitForSeconds(machineGunWaitTime);
        machineGunActive = true;
    }

    IEnumerator singleShotGunActive()
    {
        machinegunBulletInstance = Instantiate(singleShotBullet, singleShotGunSpawnPoint.transform.position, Quaternion.identity);
        bulletRB = machinegunBulletInstance.GetComponent<Rigidbody>();
        bulletRB.AddForce(transform.forward * Random.Range(bullVel - randomBulletDistance, bullVel + randomBulletDistance));
        //Lerp Things
        float distCovered = (Time.time - startTime) * lerpSpeed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarkerHeavy.position, endMarkerHeavy.position, fracJourney);
        yield return new WaitForSeconds(machineGunWaitTime);
    }

}
