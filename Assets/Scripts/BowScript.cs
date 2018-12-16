using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class BowScript : MonoBehaviour
 {
 
     public float pullSpeed;
     public GameObject ArrowPrefab;
     public Transform arrowSpawn;

     public Animator animator;
     public float reload_time = 1;
     
     
     
     private float next_shoot = 0;
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
         if (next_shoot < Time.time )
         {
             shootLogic();
         }
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
        
         
         
         if (Input.GetMouseButtonDown(0) && arrowSlotted == false)
         {
             SpawnArrow();
         }
         
         if (Input.GetMouseButton(0) && arrowSlotted == true)
         {
             if (arrowPower < 100)
             {
                 var delta = Time.deltaTime * pullSpeed;
                 arrowPower += delta;
                 arrow.transform.position += arrow.transform.forward*(delta)/100;
                 animator.SetBool("isPulled", true);
                 animator.SetFloat("pullPower", (arrowPower/100)+0.05f);
             }
         }
         
         if (Input.GetMouseButtonUp(0))
         {
             animator.SetBool("isPulled", false);
             arrowSlotted = false;
             arrow.GetComponent<Rigidbody>().isKinematic = false;
             arrow.transform.parent = null;
             arrow.transform.GetComponent<ArrowScript>().startArrow(arrowPower+0.05f);
             arrowPower = 0;
             next_shoot = Time.time + reload_time;
             arrow = null;
         }
     }
     
 }