using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Animator myAnim;
    
    
    void Start()
    {
        myAnim = GetComponentInChildren<Animator>();

        cam = GameObject.Find("Main Camera");
        myRigidbody = GameComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public float maxSpeed;
    public float normalSpeed = 4.0f;
    public float sprintSpeed = 8.0f;
    public float rotation = 0.0f;
    public float camRotation = 0.0f;
    public float rotationSpeed = 2.0f;
    public float camRotationSpeed = 1.5f; 
    GameObject cam;
    Rigidbody myRigidbody;

    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 300.0f;
    
    void Update()
    {
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
        myAnim.SetBool("isOnGround", isOnGround);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("jumped");
            myRigidBody.AddForce(transform.up * jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            maxSpeed = sprintSpeed;
        }else
        {
            maxSpeed = normalSpeed;
        }

        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed +
                              (transform.right * Input.GetAxis("Horizontal") * maxSpeed);
        myAnim.SetFloat("speed", newVelocity.magnitude);

        myRigidBody.velocity = new Vector3(newVelocity.x, myRigidBody.velocity.y, newVelocity.z);
        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed
    }
}
