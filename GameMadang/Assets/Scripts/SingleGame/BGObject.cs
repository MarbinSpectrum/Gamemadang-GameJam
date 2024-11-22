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
    private void Update()
    {
        if(isMouseOver)
        {
            sprite.color = translucentColor;
        }
    }
    private void OnMouseEnter()
    {
        isMouseOver = true;
        sprite.color = translucentColor;
    }
    private void OnMouseExit()
    {
        isMouseOver = false;
        sprite.color = originColor;
    }
}
