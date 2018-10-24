using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ArrowScript : MonoBehaviour {
 
    
    private enum ArrowState
    {
        inSpawn,
        isSticked,
        isInFlight
    }


    private ArrowState _arrowState = ArrowState.inSpawn;
    
    public float power = 10;

    private Rigidbody rBody;
    private Transform stickingPoint;
    
    // Use this for initialization
    private void Start ()
    {
        rBody = GetComponent<Rigidbody>();
        _arrowState = ArrowState.inSpawn;
    }
    	
    // Update is called once per frame
    void Update () {
        if (_arrowState == ArrowState.isInFlight)
        {
            SpinInAir();
        }
        
        if (_arrowState == ArrowState.isSticked && stickingPoint != null)
        {
            transform.position = stickingPoint.transform.position;
            transform.rotation = stickingPoint.transform.rotation;
        }
    }
 
    
    private void OnCollisionEnter(Collision other)
    {
        if (_arrowState == ArrowState.isInFlight)
        {
            StickToObstacle(other);
        }

    }

    public void startArrow(float force)
    {
        _arrowState = ArrowState.isInFlight;
        rBody.AddRelativeForce((power*force) * Vector3.forward, ForceMode.Impulse);
    }

    private void SpinInAir()
    {   
        var yVelocity = rBody.velocity.y;
        var xVelocity = rBody.velocity.x;
        var zVelocity = rBody.velocity.z;

        var combinedVelocity = Mathf.Sqrt(Mathf.Pow(xVelocity, 2) + Mathf.Pow(zVelocity, 2));

        var fallAngle = Mathf.Atan2(yVelocity, combinedVelocity) * 180 / Mathf.PI;
        
        transform.eulerAngles = new Vector3(fallAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    
    private void StickToObstacle(Collision coll)
    {
        GameObject anchor = new GameObject("ARROW_ANCHOR");
        anchor.transform.position = this.transform.position;
        anchor.transform.rotation = this.transform.rotation;
        anchor.transform.parent = coll.transform;
        this.stickingPoint = anchor.transform;
        Destroy(rBody);
        Destroy(GetComponent<Collider>());
        _arrowState = ArrowState.isSticked;
    }

}