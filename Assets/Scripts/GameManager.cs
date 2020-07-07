using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Rigidbody currentPiece;
    private Vector3 spawnLocation = new Vector3(0, 0.123f, -0.65f);

    public bool hasTakenShot = false;
    public bool isMovingPiece = false;

    public GameObject playerPiece;
    [SerializeField] private float shotStrength = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewPiece();
    }

    private void Update()
    {
        if(hasTakenShot && currentPiece.IsSleeping() && Input.GetKeyDown(KeyCode.S))
        {
            SpawnNewPiece();
        }
    }

    public void StartMovingPiece(Vector3 position)
    {
        isMovingPiece = true;
        position.y = 0.123f;
        currentPiece.isKinematic = true;
        currentPiece.MovePosition(position);
    }

    public void StopMovingPiece()
    {
        isMovingPiece = false;
        currentPiece.isKinematic = false;
    }

    public void TakeShot(Vector3 direction)
    {
        if (!hasTakenShot && !isMovingPiece)
        {
            currentPiece.AddForce(direction * shotStrength, ForceMode.Impulse);
            hasTakenShot = true;
        }
    }

    private void SpawnNewPiece()
    {
        GameObject newPiece = Instantiate(playerPiece, spawnLocation, playerPiece.transform.rotation);
        currentPiece = newPiece.GetComponent<Rigidbody>();
        hasTakenShot = false;
    }
}
