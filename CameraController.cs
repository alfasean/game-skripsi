using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minSize = 3f; 
    public float maxSize = 10f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize += scroll * zoomSpeed;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSize, maxSize);
    }
}
