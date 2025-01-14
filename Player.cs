using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    private float defaultSpeed;
    private float sprintSpeed ;
    private bool isGrounded;
    public bool canSprint = true;

    public Vector3 jump;
    private float jumpForce = 2.0f;

    private bool etatCam = false;

    public GameObject mainCam;
    public GameObject thirdCam;

    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

    }

    public void OnCollisionStay(Collision collision){
        isGrounded = true;
    }
    void OnCollisionExit(Collision collision){
    isGrounded = false;
}

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * defaultSpeed;
        float translation2 = Input.GetAxis("Horizontal") * defaultSpeed;
        translation *= Time.deltaTime;
        translation2 *= Time.deltaTime;
        if(isGrounded == true){
            transform.Translate(translation2, 0, translation);
            sprint();
            if(Input.GetKeyDown(KeyCode.Space)){
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                canSprint = false ;
            }
        }
        switchCamera();
        respawn();

    }
    void respawn(){
        if(Input.GetKey(KeyCode.F)){
            transform.position = new Vector3(0, 1, 0);
        }
    }
    void sprint(){
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded == true){
            defaultSpeed = 10.00f;
        }
        else{
            defaultSpeed = 4.30f;
        } 
    }
    void switchCamera(){
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Touche X pressée");
            if (mainCam == null || thirdCam == null)
            {
                Debug.LogError("Les caméras ne sont pas assignées !");
                return;
            }

            if (etatCam)
            {
                Debug.Log("Passage à la caméra principale.");
                mainCam.SetActive(true);
                thirdCam.SetActive(false);
            }
            else
            {
                Debug.Log("Passage à la caméra secondaire.");
                mainCam.SetActive(false);
                thirdCam.SetActive(true);
            }

            etatCam = !etatCam;
        }
    }
}
