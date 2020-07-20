using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GamePiece"))
        {
            Debug.Log("Twensko!");
        }
    }
}
