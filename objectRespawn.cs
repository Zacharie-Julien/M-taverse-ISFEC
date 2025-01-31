using Unity.Mathematics;
using UnityEngine;

public class objectRespawn : MonoBehaviour
{   

    [SerializeField] private Vector3 oldPosition;
    [SerializeField] private quaternion oldRotation;

    void Start()
    {
        oldPosition = transform.position;
        oldRotation = transform.rotation;
    }

    void Update() 
    {
        if (transform.position.y < -1f)
        {
            transform.position = oldPosition;
            transform.rotation = oldRotation;
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
