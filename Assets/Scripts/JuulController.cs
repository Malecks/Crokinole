using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuulController : MonoBehaviour
{
    private float maxTorque = 10.0f;
    private float minJump = 4.0f;
    private float maxJump = 6.5f;

    private void OnMouseDown()
    {
        Debug.Log("Did Click Juul");
        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), ForceMode.Impulse);

        Debug.Log("Did Click Juul");
    }

    Vector3 RandomTorque()
    {
        return new Vector3(
            Random.Range(-maxTorque, maxTorque),
            Random.Range(-maxTorque, maxTorque),
            Random.Range(-maxTorque, maxTorque)
            );
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minJump, maxJump);
    }
}
