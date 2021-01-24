using UnityEngine.EventSystems;
using UnityEngine;


[RequireComponent(typeof(Collider))] //A collider is needed to receive clicks
public class SettingsButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEventsManager._instance.MenuToSettings();
    }
}
