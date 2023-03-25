using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f;
    
    private float minX = -25.0f;
    private float maxX = 25.0f;
    private float minZ = -25.0f;
    private float maxZ = 25.0f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        targetPosition = GetRandomPosition();
        targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 1.0f)
        {
            targetPosition = GetRandomPosition();
            targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, Mathf.Clamp(transform.position.z, minZ, maxZ));
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(minX, maxX), 0.0f, Random.Range(minZ, maxZ));
    }
}
