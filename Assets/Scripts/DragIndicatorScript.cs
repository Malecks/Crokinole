using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragIndicatorScript : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private float startY = 0.05f;
    [SerializeField] private float endY = 0.25f;

    private void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    public void StartDrag(Vector3 startPos, Vector3 endPos)
    {
        lr.enabled = true;

        startPos.y += startY;
        endPos.y += endY;

        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
    }

    public (Vector3 startPos, Vector3 endPos) EndDrag()
    {
        (Vector3, Vector3) finalPos = (lr.GetPosition(0), lr.GetPosition(1));
        lr.enabled = false;
        return finalPos;
    }
}
