using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport1 : MonoBehaviour
{
    public Transform Start2;

    public GameObject End1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == End1)
        {
            Vector3 targetPosition = Start2.position;
            transform.position = targetPosition;
        }
    }
}
