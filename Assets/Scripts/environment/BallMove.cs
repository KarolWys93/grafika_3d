using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{

	public float speed = 1;

	// Use this for initialization
	void Start ()
	{
//		GetComponent<Rigidbody>().rotation = Quaternion.Euler(speed, 0, 0);
		GetComponent<Rigidbody>().AddTorque(speed, 0, speed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
