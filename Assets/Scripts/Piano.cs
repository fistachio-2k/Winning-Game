using UnityEngine;
using DG.Tweening;

public class Piano : MonoBehaviour
{
    [SerializeField] private Transform _positionToMove;

    public void MovePiano()
    {
        transform.DOMoveZ(_positionToMove.position.z, 2f);
        transform.DOMoveX(_positionToMove.position.x, 4f);
    }
    
}
