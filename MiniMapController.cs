using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    public Transform target; // Target yang akan diikuti oleh kamera mini map
    public float zoomSpeed = 1f; // Kecepatan zoom
    public float minZoom = 1f; // Tingkat zoom minimum
    public float maxZoom = 10f; // Tingkat zoom maksimum

    private Camera miniMapCamera;

    void Start()
    {
        miniMapCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // Zoom in saat tombol plus ditekan
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Zoom(-1f);
        }

        // Zoom out saat tombol minus ditekan
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Zoom(1f);
        }
    }

    void LateUpdate()
    {
        // Memperbarui posisi kamera mini map sesuai dengan posisi target
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
    }

    public void Zoom(float increment)
    {
        // Menghitung tingkat zoom baru
        float newZoom = miniMapCamera.orthographicSize + (zoomSpeed * increment);
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        // Mengatur tingkat zoom baru
        miniMapCamera.orthographicSize = newZoom;
    }
}
