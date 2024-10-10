using System;
using UnityEngine;

public class InputHandler 
{
    public event Action<Lemming> OnLemmingClicked;
    public event Action<Lemming> OnLemmingUnclicked;
    public event Action<Vector3> OnMousebuttonDownStayed;

    private Lemming _lemming;
    private LayerMask _hanglayerMask;
    private LayerMask _lemmingLayerMask;

    public InputHandler(LayerMask hanglayerMask, LayerMask lemmingLayerMask)
    {
        _hanglayerMask = hanglayerMask;
        _lemmingLayerMask = lemmingLayerMask;
    }
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, float.MaxValue, _lemmingLayerMask);

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<LemmingInputCollider>(out LemmingInputCollider lemmingInputCollider))
                    {
                        _lemming = lemmingInputCollider.Lemming;
                        OnLemmingClicked?.Invoke(_lemming);
                    }
                }
        }

        if(Input.GetMouseButton(0))
        {
            if (_lemming != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.MaxValue, _hanglayerMask))
                {
                    OnMousebuttonDownStayed?.Invoke(hit.point);
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (_lemming != null)
            {
                OnLemmingUnclicked?.Invoke(_lemming);
                _lemming = null;
            }
        }
    }
}
