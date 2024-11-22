using UnityEngine;

public class TestMouse : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePosition;

    private int layerMask;
    private BGObject bgObj;

    private void Awake()
    {
        cam = Camera.main;
        layerMask = (1 << 6) + (1 << 8);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition ;
        mousePosition=cam.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 10f, layerMask);

        if(hit)
        {
            if (hit.collider.gameObject.layer == 8)//배경 반투명
            {
                if(hit.collider.gameObject.TryGetComponent<BGObject>(out BGObject bGObject))
                {
                    bgObj = bGObject;
                    bGObject.OnMouse();
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Unit");
            }
        }
        else
        {
            if(bgObj !=null)
            {
                bgObj.OutMouse();
                bgObj = null;
            }
        }
    }
}
