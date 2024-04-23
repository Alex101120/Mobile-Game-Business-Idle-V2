using System.Collections.Generic;
using UnityEngine;

public class LineGraph : MonoBehaviour
{
    public float width = 0.1f; // L??imea liniilor graficului
    public float height = 5f; // În?l?imea maxim? a graficului
    public AnimationCurve curve;
    public List<float> values; // Lista de valori pentru grafic

    private LineRenderer lineRenderer;

    void Start()
    {
        // Ad?ug?m un obiect LineRenderer pentru a desena graficul
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = width;
        lineRenderer.positionCount = values.Count;

        // Desen?m punctele graficului
        for (int i = 0; i < values.Count; i++)
        {
            float t = i / (float)(values.Count - 1);
            float x = Mathf.Lerp(-5f, 5f, t); // Definim domeniul x
            float y = curve.Evaluate(t) * height * values[i]; // Evalu?m func?ia pentru fiecare punct ?i scal?m rezultatul

            // Set?m pozi?ia fiec?rui punct relativ la pozi?ia obiectului acestui script
            lineRenderer.SetPosition(i, transform.position + new Vector3(x, y, 0f));
        }
    }
}
