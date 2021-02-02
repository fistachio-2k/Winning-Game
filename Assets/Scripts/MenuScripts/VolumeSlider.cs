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
        float max = 0.4f;
        x = Mathf.Clamp(x, min, max);
        Vector3 sliderPos = transform.transform.position;
        sliderPos.x = x;
        transform.transform.position = sliderPos;
        value = Mathf.Abs(min - x) / Mathf.Abs(min - max);
        Debug.Log("x: " + x + " val:" + value);
        audioManager.updateSounds();
    }
}
