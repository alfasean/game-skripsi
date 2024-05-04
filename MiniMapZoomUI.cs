using UnityEngine;
using UnityEngine.UI;

public class MiniMapZoomUI : MonoBehaviour
{
    public Image miniMapImage;
    public Slider zoomSlider;
    public float minZoom = 1f;
    public float maxZoom = 3f;
    private Vector2 initialSize;

    void Start()
    {
        initialSize = miniMapImage.rectTransform.sizeDelta;
        zoomSlider.onValueChanged.AddListener(delegate { Zoom(); });
    }

    void Zoom()
    {
        float zoomLevel = zoomSlider.value;
        float newZoom = Mathf.Lerp(minZoom, maxZoom, zoomLevel);

        // Tetapkan ukuran gambar MiniMap ke ukuran awal dan sesuaikan dengan tingkat zoom
        miniMapImage.rectTransform.sizeDelta = initialSize * newZoom;
    }
}
