using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Rigidbody currentPiece;

    public bool hasTakenShot = false;
    public bool isMovingPiece = false;
    private bool turnShouldFinish = false;

    [SerializeField] private float shotStrength = 15.0f;

    [SerializeField] private List<PlayerController> players;

    private int playerTurn;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
           players.Add(player.GetComponent<PlayerController>());
        }

        playerTurn = 0;
        SpawnNewPiece();
    }

    private void Update()
    {
        if(hasTakenShot && currentPiece.IsSleeping() && Input.GetKeyDown(KeyCode.S))
        {
            SpawnNewPiece();
        }

        if(turnShouldFinish && currentPiece.IsSleeping())
        {
            FinishTurn();
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
            turnShouldFinish = true;
        }
    }

    public void FinishTurn()
    {
        turnShouldFinish = false;

        if (playerTurn < players.Count - 1)
        {
            playerTurn++;
        } else
        {
            playerTurn = 0;
        }

        for (int i = 0; i < players.Count - 1; i++)
        {
            players[i].playerCamera.enabled = i == playerTurn;
        }
    }

    private void SpawnNewPiece()
    {
        PlayerController currentPlayer = players[playerTurn];
        GameObject playerPiece = players[playerTurn].pieces;
        
        GameObject newPiece = Instantiate(playerPiece, currentPlayer.transform.position, playerPiece.transform.rotation);
        currentPiece = newPiece.GetComponent<Rigidbody>();
        hasTakenShot = false;
    }

    public bool IsCurrentPlayer(PlayerController player)
    {
        return player.Equals(players[playerTurn]);
    }
}

public class Player
{
    public PlayerController Controller;
    public Camera Camera;
    public GameObject Pieces;

    public Player(PlayerController controller, Camera camera, GameObject pieces)
    {
        Controller = controller;
        Camera = camera;
        Pieces = pieces;
    }
}
