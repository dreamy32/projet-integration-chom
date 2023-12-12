using UnityEngine;

[RequireComponent(typeof(Collider))]
public class QuickSaveOnTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private bool _hasBeenSaved = false;
    
    private void OnTriggerEnter(Collider c)
    {
        if (_hasBeenSaved)
            return;
        if (GameTools.CompareLayers(layerMask, c.gameObject.layer))
        {
            Debug.Log("Saved !");
            QuickSaveManager.Save();
            _hasBeenSaved = true;
        }
    }
}
