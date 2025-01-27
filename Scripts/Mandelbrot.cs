using UnityEngine;

public class Mandelbrot : MonoBehaviour
{
    public int width = 800;
    public int height = 800;
    public float zoom = 1f;
    public Vector2 offset = Vector2.zero;
    public int maxIterations = 100;

    private Texture2D mandelbrotTexture;

    void Start()
    {
        mandelbrotTexture = new Texture2D(width, height);
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<MeshRenderer>().sharedMaterial.mainTexture = mandelbrotTexture;
        }
        else
        {
            Debug.LogError("Renderer component missing from this GameObject.");
        }
        GenerateMandelbrot();
    }


    internal void GenerateMandelbrot()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Map pixel coordinate to complex plane
                float real = (x - width / 2) / zoom + offset.x;
                float imaginary = (y - height / 2) / zoom + offset.y;
                int iterations = MandelbrotIterations(real, imaginary);

                // Color based on iterations
                Color color = Color.black;
                if (iterations < maxIterations)
                {
                    float t = (float)iterations / maxIterations;
                    color = Color.HSVToRGB(t, 1, t); // Smooth color transition
                }

                mandelbrotTexture.SetPixel(x, y, color);
            }
        }
        mandelbrotTexture.Apply();
    }

    int MandelbrotIterations(float real, float imaginary)
    {
        float zReal = real, zImaginary = imaginary;
        int iterations = 0;

        while (zReal * zReal + zImaginary * zImaginary <= 4 && iterations < maxIterations)
        {
            float tempReal = zReal * zReal - zImaginary * zImaginary + real;
            zImaginary = 2 * zReal * zImaginary + imaginary;
            zReal = tempReal;
            iterations++;
        }

        return iterations;
    }
}