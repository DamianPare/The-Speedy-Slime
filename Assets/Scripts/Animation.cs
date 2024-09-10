using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    private void Awake()
    {
        offset = transform.position - target.position;
    }
    private void Update()
    {
        transform.position = target.position + offset;
    }
}
