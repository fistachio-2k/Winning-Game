using UnityEngine.EventSystems;
using UnityEngine;

public class ResumeButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(GameEventsManager._instance.ToggleSettings());
    }
}
