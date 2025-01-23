
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined 
{
    public GameObject playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        }
    }
}
