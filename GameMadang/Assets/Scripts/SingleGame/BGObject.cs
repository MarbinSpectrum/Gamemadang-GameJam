using UnityEngine;

public class BGObject : MonoBehaviour
{
    SpriteRenderer sprite;
    Color translucentColor;
    Color originColor;

    bool isMouseOver=false;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        originColor = sprite.color;
        translucentColor = new Color(originColor.r, originColor.g, originColor.b, 0.5f);
    }
   
    public void OnMouse()
    {
        sprite.color = translucentColor;
    }
    public void OutMouse()
    {
        sprite.color = originColor;
    }
}
