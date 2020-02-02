using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoomer : MonoBehaviour
{
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float targetSize;
    [SerializeField] float moveRate;
    [SerializeField] float zoomRate;
    [SerializeField] float threshold = 0.02f;
    [SerializeField] MonoBehaviour[] enableWhenDying;
    [SerializeField] Collider2D enableColliderWhenDying;
    private Camera camera;
    private bool shouldZoom = false;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            shouldZoom = true;
        }
        if (shouldZoom)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveRate);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetSize, zoomRate);
            camera.Render();

            if(Vector2.Distance(transform.position, targetPosition) < threshold && Mathf.Abs(camera.orthographicSize - targetSize) < threshold)
            {
                transform.position = targetPosition;
                camera.orthographicSize = targetSize;
                if (enableWhenDying.Length != 0)
                {
                    foreach (var script in enableWhenDying)
                        script.enabled = true;
                }
                if (enableColliderWhenDying)
                    enableColliderWhenDying.enabled = true;
                Destroy(this);
            }
        }
    }
}
