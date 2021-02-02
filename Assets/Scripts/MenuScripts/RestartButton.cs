using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex);
    }
}
