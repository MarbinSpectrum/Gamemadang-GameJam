using UnityEngine;

public class TestMouse : MonoBehaviour
{
    Camera cam;
    Vector3 mousePosition;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition ;
        mousePosition=cam.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 1 << 6);

        if(hit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("UnitHit");
            }
        }
    }
}
