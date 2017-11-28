using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollowCustom : MonoBehaviour {

    public Transform target;
    public Transform lookTarget;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);

        Transform boy = transform;
        transform.LookAt(lookTarget.position);

        transform.rotation = Quaternion.Lerp(boy.rotation, transform.rotation, Time.deltaTime * rotateSpeed);
    }
}
