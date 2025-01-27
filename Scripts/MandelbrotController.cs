using UnityEngine;

public class MandelbrotController : MonoBehaviour
{
    public Material mandelbrotMaterial;

    [Range(1, 500)]
    public int iterations = 100;
    public float zoom = 1.0f;
    public Vector2 offset = Vector2.zero;

    private Vector2 lastMousePosition;

    void Update()
    {
        // Update material properties
        mandelbrotMaterial.SetFloat("_Iterations", iterations);
        mandelbrotMaterial.SetFloat("_Zoom", zoom);
        mandelbrotMaterial.SetVector("_Offset", offset);

        // Zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            zoom *= 1.0f + scroll;
        }

        // Pan with right mouse button
        if (Input.GetMouseButton(1))
        {
            Vector2 mouseDelta = (Vector2)Input.mousePosition - lastMousePosition;
            offset -= mouseDelta * 0.01f / zoom;
        }

        lastMousePosition = Input.mousePosition;
    }
}