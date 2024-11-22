using UnityEngine;

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
            if (hit[i].collider.gameObject.layer == 8)
            {
                if (hit[i].collider.gameObject.TryGetComponent<BGObject>(out BGObject bGObject))
                {
                    bgObj = bGObject;
                    bGObject.OnMouse();
                }
            }
            else
            {
                if (bgObj != null)
                {
                    bgObj.OutMouse();
                    bgObj = null;
                }
            }

            if(Input.GetMouseButtonDown(0))
            {
                if (hit[i].collider.gameObject.layer == 9)
                {
                    Debug.Log("정답");
                    return;
                }
                else if (hit[i].collider.gameObject.layer == 6)
                {
                    GameManager.Instance.OnLife();
                    Debug.Log("오답");
                    return;

                }
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
