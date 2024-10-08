using System;
using UnityEngine;

public class InputHandler 
{
    public event Action<Lemming> OnLemmingClicked;
   public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.TryGetComponent<LemmingInputCollider>(out LemmingInputCollider lemmingInputCollider))
                    OnLemmingClicked?.Invoke(lemmingInputCollider.Lemming);
            }
        }
    }
}
