using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMovement : MonoBehaviour
{
    public float verticalSpeed = 1;
    public float horizontalSpeed = 1;
    public float lerpSpeed = 1;
    public bool looping = false;
    private CameraManager cameraManager;
    private Vector2 originalPosition;
    private Vector2 originalTargetPosition;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        originalPosition = new Vector2(transform.position.x, transform.position.y);
        originalTargetPosition = new Vector2(cameraManager.transform.position.x, cameraManager.transform.position.y);
        screenBounds = cameraManager.gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraManager.transform.position.z));
        if (looping)
        {
            loadChildObjects();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(originalPosition.x + (cameraManager.transform.position.x - originalTargetPosition.x) * horizontalSpeed, originalPosition.y + (cameraManager.transform.position.y - originalTargetPosition.y) * verticalSpeed, transform.position.z);
        transform.position = Vector3.MoveTowards(
            transform.position,
            newPosition,
            Time.deltaTime * lerpSpeed
        );
    }

    void LateUpdate()
    {
        if(looping)
        {

            screenBounds = cameraManager.gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraManager.transform.position.z));
            repositionChildObjects();
        }
    }

    void loadChildObjects()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            float objectWidth = child.GetComponent<SpriteRenderer>().bounds.size.x;
            GameObject c = Instantiate(child.gameObject, transform);
            c.transform.position = new Vector3(objectWidth, child.position.y, child.position.z);
        }
    }
    void repositionChildObjects()
    {
        int count = transform.childCount;
        if (count > 1)
        {
            Transform firstChild = transform.GetChild(0);
            Transform lastChild = transform.GetChild(count - 1);
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.position.x + halfObjectWidth * 2, lastChild.position.y, lastChild.transform.position.z);
            }
        }
    }
}
