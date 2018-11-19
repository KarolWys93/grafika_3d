using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class BowScript : MonoBehaviour
 {
 
     public float pullSpeed;
     public GameObject ArrowPrefab;
     public Transform arrowSpawn;


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
         
         
//         if (!arrowSlotted)
//         {
//             SpawnArrow();
//         }
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
         
         if (Input.GetButton("Fire1"))
         {
            arrowPower = Input.GetAxis("Fire1");
             //arrowPower += Time.deltaTime * pullSpeed;
             //if (arrowPower >= 100)
             //{
             //    arrowPower = 100;
             //}
        }
         
         if (Input.GetButtonUp("Fire1"))
         {
            arrowPower = Input.GetAxisRaw("Fire1");
            arrowSlotted = false;
             arrow.GetComponent<Rigidbody>().isKinematic = false;
             arrow.transform.parent = null;
             _arrowScript.startArrow(arrowPower+0.05f);// = _arrowScript.shootPower * ((pullAmount / 100) + 0.05f);
             arrowPower = 0;
         }
         
         if (Input.GetButtonDown("Fire1") && arrowSlotted == false)
         {
             SpawnArrow();
         }
         
         
     }
     
     
//     void shotLogic(){
//
////        SkinnedMeshRenderer _bowSkin = bow.transform.GetComponent<SkinnedMeshRenderer>();
////        SkinnedMeshRenderer _arrowSkin = arrow.transform.GetComponent<SkinnedMeshRenderer>();
//         Rigidbody _arrowRigidB = arrow.transform.GetComponent<Rigidbody>();
//
//         ShootArrowScript _arrowScript = arrow.transform.GetComponent<ShootArrowScript>();//GetComponent<ShootArrowScript>();
//
//         if (Input.GetMouseButton(0))
//         {
//             pullAmount += Time.deltaTime * pullSpeed;
//         }
//
//         if (Input.GetMouseButtonUp(0))
//         {
//             arrowSlotted = false;
//             _arrowRigidB.isKinematic = false;
//             arrow.transform.parent = null;
//             _arrowScript.shootPower = _arrowScript.shootPower * ((pullAmount / 100) + 0.05f);
//             pullAmount = 0;
//         }
//        
////        _bowSkin.SetBlendShapeWeight(0, pullAmount);
////        _arrowSkin.SetBlendShapeWeight(0, pullAmount);
//
//         _arrowScript.enabled = true;
//
//         if (Input.GetMouseButtonDown(0) && arrowSlotted == false)
//         {
//             SpawnArrow();
//         }
//
//
//     }
     
     
 }