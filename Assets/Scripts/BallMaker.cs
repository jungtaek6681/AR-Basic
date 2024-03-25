using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallMaker : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float shootPower;

    private void OnTouch(InputValue value)
    {
        GameObject instance = Instantiate(ballPrefab, Camera.main.transform.position, Quaternion.identity);

        Rigidbody rigid = instance.GetComponent<Rigidbody>();
        if (rigid != null)
        {
            rigid.AddForce(Camera.main.transform.forward * shootPower, ForceMode.Impulse);
        }

        Destroy(instance, 10f);
    }
}
