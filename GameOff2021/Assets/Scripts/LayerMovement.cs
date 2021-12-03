using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMovement : MonoBehaviour
{
    public float verticalSpeed = 1;
    public float horizontalSpeed = 1;
    public float lerpSpeed = 1;
    public bool looping = true;
    //private CameraManager cameraManager;
    private Vector2 originalPosition;
    private Vector2 originalTargetPosition;
    //private Vector2 screenBounds;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        //cameraManager = FindObjectOfType<CameraManager>();
        player = FindObjectOfType<Player>();
        originalPosition = new Vector2(transform.position.x, transform.position.y);
        originalTargetPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        //screenBounds = cameraManager.gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraManager.transform.position.z));
        if (looping)
        {
            loadChildObjects();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(originalPosition.x + (player.transform.position.x - originalTargetPosition.x) * horizontalSpeed, originalPosition.y + (player.transform.position.y - originalTargetPosition.y) * verticalSpeed, transform.position.z);
        transform.position = Vector3.Lerp(
            transform.position,
            newPosition,
            Time.deltaTime * lerpSpeed
        );
    }

    void LateUpdate()
    {
        if(looping)
        {
            //screenBounds = cameraManager.gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraManager.transform.position.z));
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
            if (player.transform.position.x > lastChild.transform.position.x)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.position.x + halfObjectWidth * 2, lastChild.position.y, lastChild.transform.position.z);
            }
        }
    }
}
