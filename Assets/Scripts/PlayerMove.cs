using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public float sensitivityX = 5F;
    public float sensitivityY = 5F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    private void Start()
    {
        Cursor.visible = false;
    }

    void FixedUpdate ()
    {
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Horizontal") * sensitivityX;

        rotationY += Input.GetAxis("Vertical") * sensitivityY;
        rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);

        Vector3 movement = new Vector3(-Input.GetAxis("Move X") * 0.1f, 0.0f, Input.GetAxis("Move Z") * 0.1f).normalized;
        movement = transform.TransformDirection(movement);

        GetComponent<Rigidbody>().MovePosition(transform.position + movement);
    }
}