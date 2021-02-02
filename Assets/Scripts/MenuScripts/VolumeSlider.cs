using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(Collider))] //A collider is needed to receive clicks
public class VolumeSlider : MonoBehaviour, IDragHandler
{
    [SerializeField] private AudioManager audioManager;
    public float value = 0.25f;
    public void OnDrag(PointerEventData eventData)
    {
        float x = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane)).x;
        float min = -0.8f;
        float max = 0.8f;
        float min2 = -0.1f;
        float max2 = 0.03f;
        x = Mathf.Clamp(x, min2, max2);
        value = Mathf.Abs(min2 - x) / Mathf.Abs(min2 - max2);
        value = Mathf.Clamp(value, min, max);
        Vector3 sliderPos = transform.transform.position;
        sliderPos.x = min + value * Mathf.Abs(min - max);
        transform.transform.position = sliderPos;
        Debug.Log("x: " + x + " val:" + value);
        audioManager.updateSounds();
    }
}
