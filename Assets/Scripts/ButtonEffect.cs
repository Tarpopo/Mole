using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    Vector3 size = new Vector2();
    Vector3 downSize;

    [SerializeField]
    float scaler = 0.9f;

    public bool IsDowned { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        Down();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Up();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Up();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Up();
    }
    void Down()
    {
        CalculateSize();

        transform.localScale = downSize;
    }
    void Up()
    {
        transform.localScale = size;

        IsDowned = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CalculateSize();
    }

    void CalculateSize()
    {
        size = transform.localScale;
        downSize = size * scaler;

        IsDowned = true;
    }
}
