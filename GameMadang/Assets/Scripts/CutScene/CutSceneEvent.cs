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

    [SerializeField] private Transform shootTransform;
    [SerializeField] private GameObject image;
    Sequence sequence;

    private void Awake()
    {
        text=transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    public void StartCutScene()
    {
        RandomText();
        image.SetActive(true);
    }
    public void EndCutScene()
    {
        gameObject.SetActive(false);
    }
    //����Ʈ�� �ϳ��� ��ġ�� 
    public void ShootSFX()
    {
        Time.timeScale = 0f;

        GameObject test1 = Instantiate(shootVFX, shootTransform);//SFX ����
        GameObject test2 = Instantiate(explosionVFX, shootTransform);//SFX ����

        test2.SetActive(false);

        Vector2 mousePosition = GameManager.Instance.clickPosition;//Ŭ���� ��ǥ �ҷ�����


        test1.transform.position = new Vector3(test1.transform.position.x, test1.transform.position.y, 0); // Z�� ����

        Vector2 direction = mousePosition - (Vector2)shootTransform.position;//���⺤��
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        sequence = DOTween.Sequence().SetUpdate(true)
       .Append(test1.transform.DORotate(new Vector3(0, 0, angle), 0.01f).SetEase(Ease.Linear))
       .Append(test1.transform.DOMove(mousePosition, 0.5f))
       .Append(DOTween.To(() => 0f, x => { test1.SetActive(false); }, 1f, 0f))
       .Join(DOTween.To(() => 0f, x => { test2.transform.position = mousePosition; }, 1f, 0f))
       .Append(DOTween.To(() => 0f, x => { test2.SetActive(true); }, 1f, 0f));
       
    }


    private void RandomText()
    {
        text.text = dialogue[Random.Range(0,dialogue.Length)];
    }

    
}
