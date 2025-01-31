using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class CubeUp : NetworkBehaviour
{

    private bool _upCubePressed = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _upCubePressed = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (_upCubePressed)
        {
            gameObject.transform.Translate(new Vector3(0, 1, 0));    
        }
        
        _upCubePressed = false;
    }

    
}
