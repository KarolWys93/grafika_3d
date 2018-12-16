﻿using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class BowScript : MonoBehaviour
 {
 
     public float pullSpeed;
     public GameObject ArrowPrefab;
     public Transform arrowSpawn;

     public Animator animator;
     


     private GameObject arrow;

     private bool arrowSlotted = false;
     
     private float arrowPower = 0;
 	
     // Use this for initialization
     void Start () {
 		SpawnArrow();
     }
 	
     // Update is called once per frame
     void Update ()
     {
         shootLogic();
     }
     
     
     private void SpawnArrow()
     {
         arrowSlotted = true;
         arrow = Instantiate(ArrowPrefab, arrowSpawn.position, arrowSpawn.rotation) as GameObject;
         arrow.transform.parent = transform;
         arrow.GetComponent<Rigidbody>().isKinematic = true;
     }

     void shootLogic()
     {
         ArrowScript _arrowScript = arrow.transform.GetComponent<ArrowScript>();
         
         if (Input.GetMouseButton(0))
         {
             animator.SetBool("isPulled", true);
             animator.SetFloat("pullPower", arrowPower/100);
             
             if (arrowPower < 100)
             {
                 float lastPower = arrowPower;
                 arrowPower += Time.deltaTime * pullSpeed;
                 arrow.transform.position -= arrow.transform.forward*(lastPower - arrowPower)/100;
             }
         }
         
         if (Input.GetMouseButtonUp(0))
         {
             animator.SetBool("isPulled", false);
             arrowSlotted = false;
             arrow.GetComponent<Rigidbody>().isKinematic = false;
             arrow.transform.parent = null;
             _arrowScript.startArrow(arrowPower+0.05f);// = _arrowScript.shootPower * ((pullAmount / 100) + 0.05f);
             arrowPower = 0;
         }
         
         if (Input.GetMouseButtonDown(0) && arrowSlotted == false)
         {
             SpawnArrow();
         }
         
         
     }
     
 }