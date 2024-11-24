using DG.Tweening;
using TMPro;
using UnityEngine;

public class CutSceneEvent : MonoBehaviour
{
    //���� ���� �ִ°� ȿ����, ������, ��纯ȯ ����?

    private TextMeshProUGUI text;
    private string[] dialogue = { "�޾ƶ� ����ȭ�� �Ѿ�!", "�ű� ��! ������� �ҿ����!","���� �׸� ���� ����� ���ư�!","�̼��� ����ȭ!" };

    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private GameObject shootVFX;

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
    //����Ʈ�� �ϳ��� ��ġ�� 
    public void ShootSFX()
    {
        //GameObject Shoo = Instantiate(shootVFX, ); ��񺸷�

        sequence = DOTween.Sequence()
       .Append(explosionVFX.transform.DOMove(Input.mousePosition, 1f).SetUpdate(true))
       .Append(DOTween.To(() => 0f, x => { explosionVFX.SetActive(false); },1f,0f)).SetUpdate(true)
       .Join(DOTween.To(() => 0f, x => { shootVFX.SetActive(true); },1f,0f).SetUpdate(true));
    }


    private void RandomText()
    {
        text.text = dialogue[Random.Range(0,dialogue.Length)];
    }

    
}
