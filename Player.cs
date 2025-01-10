using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public bool isGrounded;
    public Material mat;
    public Vector3 jump;
    public float jumpForce = 2.0f;

    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

    }
    void OnCollisionStay(){
        isGrounded = true;
    }
    // Update is called once per frame
    void Update()
    {
        // CheckKeys ();
        float translation = Input.GetAxis("Vertical") * speed;
        float translation2 = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        translation2 *= Time.deltaTime;
        transform.Translate(translation2, 0, translation);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }
    // void CheckKeys (){
	// 	if(Input.GetKey(KeyCode.LeftArrow)) {
	// 		transform.position += Vector3.left * speed;
	// 	}
	// 	if(Input.GetKey(KeyCode.RightArrow)) {
	// 		transform.position += Vector3.right * speed;
	// 	}
	// }
    // private void private void OnMouseEnter() {
        
    // }
}
