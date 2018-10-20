using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ArrowScript : MonoBehaviour {
 
    
    
    private bool __isFlight = false;
    public float power = 10;

    private Rigidbody rBody;
    private Transform anchor;
    
    // Use this for initialization
    private void Start () {
        rBody = GetComponent<Rigidbody>();
        rBody.AddRelativeForce(power * Vector3.forward, ForceMode.Impulse);
//        rBody.velocity = power*Vector3.forward;
        __isFlight = true;
    }
    	
    // Update is called once per frame
    void Update () {
        
        if (__isFlight)
        {
            SpinInAir();
        }
        else if (this.anchor != null)
        {
            transform.position = anchor.transform.position;
            transform.rotation = anchor.transform.rotation;
        }
    }

    private void FixedUpdate()
    {

    }


    private void SpinInAir()
    {
        if (!__isFlight) return;
        
        var yVelocity = rBody.velocity.y;
        var xVelocity = rBody.velocity.x;
        var zVelocity = rBody.velocity.z;

        var combinedVelocity = Mathf.Sqrt(Mathf.Pow(xVelocity, 2) + Mathf.Pow(zVelocity, 2));

        var fallAngle = Mathf.Atan2(yVelocity, combinedVelocity) * 180 / Mathf.PI;
        
        transform.eulerAngles = new Vector3(fallAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
//        StickToObstacle(other);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (__isFlight)
        {
            StickToObstacle2(other);
        }
        
    }

    private void StickToObstacle2(Collision coll)
    {
        __isFlight = false;
//        this.transform.position = coll.contacts[0].point;
//        this.childCollider.isTrigger = true;
        
        GameObject anchor = new GameObject("ARROW_ANCHOR");
        anchor.transform.position = this.transform.position;
        anchor.transform.rotation = this.transform.rotation;
        anchor.transform.parent = coll.transform;
        this.anchor = anchor.transform;
//        rBody.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(rBody);
        Destroy(GetComponent<Collider>());
//        Destroy(GetComponent<Collider>());
    }
    
//    private void StickToObstacle(Collider coll)
//    {
//        if (!__isFlight) return;
//        __isFlight = false;
//        rBody.constraints = RigidbodyConstraints.FreezeAll;
////            this.rBody.velocity = Vector3.zero;
////            this.rBody.useGravity = false;
////            this.rBody.isKinematic = true;
//
//    }
}