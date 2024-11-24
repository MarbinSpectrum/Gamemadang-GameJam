using UnityEngine;

public class SingleMouseCursor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Update()
    {
        if(Time.timeScale!=0)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            spriteRenderer.color = GameManager.Instance.OnMouseColor;
        }
       
    }

}
