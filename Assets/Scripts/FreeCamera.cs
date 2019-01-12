using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float cameraSensitivity = 90;
    public float moveSpeed = 20;
    public float climbSpeed = 4;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private GameObject FPSController;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController FPCScript;

    private GameObject bow;
    private BowScript bowScript;

    private bool isEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FPSController = GameObject.Find("FPSController");
        FPCScript = FPSController.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        bow = GameObject.Find("bow2");
        bowScript = bow.GetComponent<BowScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

            transform.position += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;


            transform.position += transform.up * Input.GetAxis("Up");
            transform.position -= transform.up * Input.GetAxis("Down");

            if (Input.GetKey(KeyCode.Q)) { transform.position += transform.up * climbSpeed * Time.deltaTime; }
            if (Input.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * Time.deltaTime; }
        }


        if (Input.GetButtonDown("Switch Camera"))
        {
            FPCScript.enabled = !FPCScript.enabled;
            bowScript.enabled = !bowScript.enabled;
            isEnabled = !isEnabled;
        }
    }
}
