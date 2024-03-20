using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private GameObject selection;
    public void SetSelected(bool state)
    {
        selection.SetActive(state);
    }
}
