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
     
     [SerializeField] private AudioClip m_PullSound;        // the sound played when arrow is pulled.
     [SerializeField] private AudioClip m_ReleaseSound;    // the sound played when arrow is released.
     private AudioSource m_AudioSource;
     
 	
     // Use this for initialization
     void Start ()
     {
        m_AudioSource = GetComponent<AudioSource>();
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

     private bool isPulled = false;
     void shootLogic()
     {
        
         
         
         if (Input.GetButtonDown("Fire1") && arrowSlotted == false)
         {
             SpawnArrow();
         }
         
         if (Input.GetButton("Fire1") && arrowSlotted == true)
         {
             if (!isPulled)
             {
                 isPulled = true;
                 m_AudioSource.clip = m_PullSound;
                 m_AudioSource.Play();
             }
             if (arrowPower < 100)
             {
                 var delta = Time.deltaTime * pullSpeed;
                 arrowPower += delta;
                 arrow.transform.position += arrow.transform.forward*(delta)/100;
                 animator.SetBool("isPulled", true);
                 animator.SetFloat("pullPower", (arrowPower/100)+0.05f);
             }
         }
         
         if (Input.GetButtonUp("Fire1"))
         {
             isPulled = false;
             m_AudioSource.Stop();
             m_AudioSource.clip = m_ReleaseSound;
             m_AudioSource.Play();
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