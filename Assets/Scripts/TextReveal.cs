using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TextReveal : MonoBehaviour
{
    private TextMeshPro _tmp;
    [SerializeField] private float _revealInterval = 3f;
    //[SerializeField] private Transform _playerTransform;
    [SerializeField] private float _triggerDistance = 3.5f;
    private Camera _cam;

    void Awake()
    {
        _cam = Camera.main;
        _tmp = GetComponent<TextMeshPro>();
        _tmp.maxVisibleCharacters = 0;
    }
    
    public IEnumerator RevealText()
    { 
        _tmp.ForceMeshUpdate();
        int totalVisableChar = _tmp.textInfo.characterCount; // Get the count of all visable characters 
        int count = 0;
        bool reveald = false;
        bool covered = false;

        // A loop which reveal and cover the text once per routine
        while (!reveald || !covered)
        {
            int visableCount = count % (totalVisableChar + 1);
            _tmp.maxVisibleCharacters = visableCount;

            if (Vector3.Distance(transform.position, _cam.transform.position) < _triggerDistance && visableCount < totalVisableChar)
            {
                count += 1;
            }
            else if (Vector3.Distance(transform.position, _cam.transform.position) >= _triggerDistance && count > 0)
            {
                count -= 1;
            }

            if (visableCount == totalVisableChar)
            {
                reveald = true;
            }
            else if (reveald && visableCount == 0)
            {
                covered = true;
            }
            yield return new WaitForSeconds(_revealInterval / totalVisableChar);
        }

        //TODO: add hide text!!!!!!
    }
}
