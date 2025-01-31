using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [Tooltip("Player Prefab")]
    [SerializeField]
    private GameObject playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab, new Vector3(2,1,-6), Quaternion.identity);
        }
    }
}
