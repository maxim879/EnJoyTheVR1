using UnityEngine;
using TMPro;

public class EVRPhotonServerPanel : MonoBehaviour
{
    [Header("UI Элементы")]
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private TMP_Text playerCountText;

    public void SetRoomInfo(string roomName, int playerCount, int maxPlayers)
    {
        if (roomNameText != null)
        {
            roomNameText.text = roomName;
        }

        if (playerCountText != null)
        {
            playerCountText.text = $"{playerCount}/{maxPlayers}";
        }
    }
}
