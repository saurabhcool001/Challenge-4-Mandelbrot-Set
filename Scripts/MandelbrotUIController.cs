using UnityEngine;
using UnityEngine.UI;

public class MandelbrotUIController : MonoBehaviour
{
    public Slider iterationSlider;
    public Text iterationText;

    public Button generateButton;

    private MandelbrotController mandelbrotController;
    void Start()
    {
        mandelbrotController = GetComponent<MandelbrotController>();
        iterationSlider.onValueChanged.AddListener(UpdateIterations);
    }

    void UpdateIterations(float value)
    {
        mandelbrotController.iterations = (int)value;
        iterationText.text = $"Iterations: {(int)value}";
    }
}