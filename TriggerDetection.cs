using Unity.VisualScripting;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    public float jumpForce = 522.0f; 
     private void OnTriggerEnter(Collider other)
    {        
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        
    }
}