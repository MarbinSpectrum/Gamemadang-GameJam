using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePosition;

    private int layerMask;
    private BGObject bgObj;

    private void Awake()
    {
        cam = Camera.main;
        layerMask = (1 << 6) + (1 << 8) +(1<<9);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition ;
        mousePosition=cam.ScreenToWorldPoint(mousePosition);

        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePosition, transform.forward, 10f, layerMask);

        for(int i=0; i<hit.Length; i++)
        {
            if (hit[i].collider.gameObject.layer == 8 && !EventSystem.current.IsPointerOverGameObject())
            {
                if (hit[i].collider.gameObject.TryGetComponent<BGObject>(out BGObject bGObject))
                {
                    bgObj = bGObject;
                    bGObject.OnMouse();
                }
            }

            if(Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
            {
                if (hit[i].collider.gameObject.layer == 9)
                {
                    GameManager.Instance.clickPosition = Input.mousePosition;
                    GameManager.Instance.OnScore();

                    return;
                }
                else if (hit[i].collider.gameObject.layer == 6)
                {
                    if (i != hit.Length-1) continue;

                    GameManager.Instance.clickPosition = Input.mousePosition;
                    GameManager.Instance.OnLife();

                    Debug.Log("¿À´ä");

                }

                GameManager.Instance.OnMouseColor = Color.red;

            }
            else
            {
                GameManager.Instance.OnMouseColor = Color.white;
            }
        }
        if(hit.Length==0)
        {
            if (bgObj != null)
            {
                bgObj.OutMouse();
                bgObj = null;
            }
        }

    }
}
