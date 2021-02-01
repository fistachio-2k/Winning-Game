using UnityEngine.EventSystems;
using UnityEngine;


[RequireComponent(typeof(Collider))] //A collider is needed to receive clicks
public class SettingsButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(GameEventsManager._instance.ToggleSettings());
    }
}
