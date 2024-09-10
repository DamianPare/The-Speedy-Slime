using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport2 : MonoBehaviour
{
    public Transform Start3;

    public GameObject End2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == End2)
        {
            Vector3 targetPosition = Start3.position;
            transform.position = targetPosition;
        }
    }
}
