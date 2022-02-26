using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLockOn : MonoBehaviour
{
    [SerializeField]
    private string selectableTag = "Selectable";
    [SerializeField]
    private Material highlightMaterial;
    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Camera MainCamera;

    private Transform _selection;

    // Update is called once per frame
    void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }
        var ray = MainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        Debug.DrawRay(MainCamera.transform.position, ray.direction * 10, Color.blue);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            var selection = hit.transform;
            Debug.Log("Selected tag: " + selection.tag);
            Debug.Log("Selectable tag: " + selectableTag);
            if (selection.CompareTag(selectableTag))
            {
                Debug.Log("Selection Hit");
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;
            }
        }
    }
}
