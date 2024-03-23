using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSync : MonoBehaviour
{
    public Canvas canvasToCopyFrom; // Referin?a c?tre Canvas-ul a c?rui pozi?ie vrei s? o copiezi
    public GameObject targetGameObject; // GameObject-ul c?ruia vrei s? îi aplici pozi?ia Canvas-ului

    void Start()
    {
        CopyCanvasPosition();
    }

    public void CopyCanvasPosition()
    {
        // Verific?m dac? avem Canvas-ul ?i GameObject-ul
        if (canvasToCopyFrom == null || targetGameObject == null)
        {
            Debug.LogError("Canvas-ul sau GameObject-ul nu sunt setate!");
            return;
        }

        // Ob?inem componenta RectTransform a Canvas-ului
        RectTransform canvasRectTransform = canvasToCopyFrom.GetComponent<RectTransform>();
        if (canvasRectTransform == null)
        {
            Debug.LogError("Componenta RectTransform nu a fost g?sit? pe Canvas-ul dat!");
            return;
        }

        // Ob?inem componenta Transform a GameObject-ului ?int?
        Transform targetTransform = targetGameObject.transform;

        // Copiem pozi?ia Canvas-ului ?i o aplic?m pe pozi?ia GameObject-ului ?int?
        targetTransform.position = canvasRectTransform.position;
    }
}
