using DG.Tweening;
using TMPro;
using UnityEngine;

public class CutSceneEvent : MonoBehaviour
{
    //넣을 수도 있는거 효과음, 움직임, 대사변환 정도?

    private TextMeshProUGUI text;
    private string[] dialogue = { "받아라 정상화의 총알!", "거기 너! 숨어봤자 소용없다!","이제 그만 너희 세계로 돌아가!","이세계 정상화!" };

    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private GameObject shootVFX;

    [SerializeField] private Transform shootTransform;
    Sequence sequence;

    private void Awake()
    {
        text=transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void StartEvent()
    {
        Time.timeScale = 0f;
        RandomText();
    }
    public void EndEvent()
    {
        gameObject.SetActive(false);
    }
    //이펙트를 하나로 합치고 
    public void ShootSFX()
    {
        GameObject test1 = Instantiate(shootVFX, shootTransform);
        Vector3 mousePosition = GameManager.Instance.clickPosition;
        test1.transform.position = new Vector3(test1.transform.position.x, test1.transform.position.y, 0); // Z축 고정
        
        Vector3 direction = mousePosition - shootTransform.position;
        direction.z = 0;


        sequence = DOTween.Sequence().SetUpdate(true)
        .Append(test1.transform.DORotate(direction, 0.1f).SetEase(Ease.Linear))
       .Append(test1.transform.DOMove(mousePosition, 1f));
       // Debug.Log($"start: {shootTransform.position}, end: {mousePosition}, angle: {z}");
        //.Append(DOTween.To(() => 0f, x => { explosionVFX.SetActive(false); },1f,0f)).SetUpdate(true)
        //.Join(DOTween.To(() => 0f, x => { shootVFX.SetActive(true); },1f,0f).SetUpdate(true));
    }

    float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }

    private void RandomText()
    {
        text.text = dialogue[Random.Range(0,dialogue.Length)];
    }

    
}
