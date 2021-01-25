using UnityEngine.EventSystems;
using UnityEngine;


[RequireComponent(typeof(Collider))] //A collider is needed to receive clicks
public class StartButton : MonoBehaviour, IPointerClickHandler
{
    public Color startColor;
    public Color mouseOverColor;
    //bool mouseOver = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEventsManager._instance.MenuToGame();
    }

    private void OnMouseEnter()
    {
        //mouseOver = true;
        GetComponent<Renderer>().material.SetColor("_Color", mouseOverColor);
    }

    private void OnMouseExit()
    {
        //mouseOver = false;
        GetComponent<Renderer>().material.SetColor("_Color", startColor);
    }
}
