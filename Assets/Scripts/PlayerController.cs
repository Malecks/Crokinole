using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    private GameManager gameManagerScript;
    private DragIndicatorScript dragIndicator;
    private GameObject shotRing;

    private float horizontalInput;

    [SerializeField] private float rotationSpeed = 30.0f;
    [SerializeField] private bool isClickingShotArea = false;
    [SerializeField] private bool startedClickInShotArea = false;

    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        dragIndicator = gameObject.GetComponent<DragIndicatorScript>();
        shotRing = GameObject.Find("ShotRing");
    }

    void Update()
    {
        bool isCurrentPlayer = gameManagerScript.IsCurrentPlayer(this);

        if (isCurrentPlayer)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            if (horizontalInput != 0 && !gameManagerScript.hasTakenShot)
            {
                shotRing.transform.Rotate(Vector3.up * -horizontalInput * Time.deltaTime * rotationSpeed);
                gameManagerScript.StartMovingPiece(gameObject.transform.position);
            }
            else
            {
                gameManagerScript.StopMovingPiece();
            }

            if (Input.GetMouseButtonDown(0) && isClickingShotArea && !gameManagerScript.hasTakenShot)
            {
                startedClickInShotArea = true;
            }

            if (Input.GetMouseButton(0) && startedClickInShotArea && !isClickingShotArea)
            {
                BeginShot();
            }

            if (Input.GetMouseButtonUp(0) && startedClickInShotArea)
            {
                EndShot(success: true);
            }

            if (Input.GetMouseButtonDown(1))
            {
                EndShot(success: false);
            }
        }

    }

    private void OnMouseDown()
    {
        isClickingShotArea = true;
    }

    private void OnMouseExit()
    {
        isClickingShotArea = false;
    }

    private void BeginShot()
    {
        startPos = gameObject.transform.position;
        endPos = GetMousePos();

        dragIndicator.StartDrag(startPos, endPos);
    }

    private void EndShot(bool success)
    {
        startedClickInShotArea = false;
        (Vector3 start, Vector3 end) = dragIndicator.EndDrag();

        if (success)
        {
            shotRing.transform.SetPositionAndRotation(shotRing.transform.position, new Quaternion(0, 0, 0, 0));

            Vector3 direction = start - end;

            Debug.Log("Direction: " + direction);
            gameManagerScript.TakeShot(direction);
        }
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = -Vector3.one;

        Camera currentCam = Camera.main;

        foreach (Camera cam in Camera.allCameras)
        {
            if (cam.isActiveAndEnabled)
            {
                currentCam = cam;
            }
        }

        Plane plane = new Plane(new Vector3(0, 0.75f, 0), 0f);
        Ray ray = currentCam.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float distanceToPlane))
        {
            mousePos = ray.GetPoint(distanceToPlane);
        }

        return mousePos;
    }

    // Other method of getting mouse position w/ RayCast (no plane)
    private Vector3 GetMousePos2()
    {
        Vector3 mousePos = -Vector3.one;
        Camera currentCam = Camera.main;

        foreach (Camera cam in Camera.allCameras)
        {
            if (cam.isActiveAndEnabled)
            {
                currentCam = cam;
            }
        }

        Ray ray = currentCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            mousePos = hit.point;
        }

        return mousePos;
    }
}
