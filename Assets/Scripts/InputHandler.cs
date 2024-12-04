using System;
using UnityEngine;

public class InputHandler 
{
    public event Action<Lemming, Vector3> OnLemmingClicked;
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
                        
                    if (_lemming != null)
                    {
                        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit1;

                        if (Physics.Raycast(ray1, out hit1, float.MaxValue, _hanglayerMask))
                        {
                            OnLemmingClicked?.Invoke(_lemming, hit1.point);
                           // OnMousebuttonDownStayed?.Invoke(hit1.point);
                        }
                    }
                }
                }
        }

        if(Input.GetMouseButtonDown(0))
        {
           
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
