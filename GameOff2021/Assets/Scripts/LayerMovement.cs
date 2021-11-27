using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMovement : MonoBehaviour
{
    public float verticalSpeed = 1;
    public float horizontalSpeed = 1;
    public float lerpSpeed = 1;
    public Transform target;
    private Vector2 originalPosition;
    private Vector2 originalTargetPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = new Vector2(transform.position.x, transform.position.y);
        originalTargetPosition = new Vector2(target.position.x, target.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(originalPosition.x + (target.position.x - originalTargetPosition.x) * horizontalSpeed, originalPosition.y + (target.position.y - originalTargetPosition.y) * verticalSpeed, transform.position.z);
        transform.position = Vector3.MoveTowards(
            transform.position,
            newPosition,
            Time.deltaTime * lerpSpeed
        );
    }
}
