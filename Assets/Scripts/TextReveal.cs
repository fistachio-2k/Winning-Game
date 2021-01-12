using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TextReveal : MonoBehaviour
{
    private TextMeshPro _tmp;
    [SerializeField] private float _revealInterval = 3f;
    [SerializeField] private Transform _playerTransform;
    
    IEnumerator Start()
    {
        // Get referance to the TMP object
        _tmp = GetComponent<TextMeshPro>();
        _tmp.ForceMeshUpdate();
        int totalVisableChar = _tmp.textInfo.characterCount; // Get the count of all visable characters 
        int count = 0;

        while (true)
        {
            int visableCount = count % (totalVisableChar + 1);
            _tmp.maxVisibleCharacters = visableCount;

            Debug.Log(Vector3.Distance(transform.position, _playerTransform.position));
            if (Vector3.Distance(transform.position, _playerTransform.position) < 5.5f && visableCount < totalVisableChar)
            {
                count += 1;
            }
            else if (Vector3.Distance(transform.position, _playerTransform.position) >= 5.5f && count > 0)
            {
                count -= 1;
            }
            yield return new WaitForSeconds(_revealInterval / totalVisableChar);
        }
    }
}
