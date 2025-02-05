using UnityEngine;
using Fusion;

public class CubeUp : NetworkBehaviour
{

    public void cubeUp(Vector3 currentPosition)
    {
        gameObject.transform.position = currentPosition;
    }

    
}
