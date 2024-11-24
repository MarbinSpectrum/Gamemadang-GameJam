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
        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
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
        Vector2 mousePosition = GameManager.Instance.clickPosition;//Ŭ���� ��ǥ �ҷ�����
        ShootSFX(mousePosition);
    }

    public void ShootSFX(Vector2 screenPos)
    {
        Time.timeScale = 0f;

        GameObject shoot = Instantiate(shootVFX, shootTransform);//SFX ����
        GameObject explosion = Instantiate(explosionVFX, shootTransform);//SFX ����

        explosion.SetActive(false);

        shoot.transform.position = new Vector3(shoot.transform.position.x, shoot.transform.position.y, 0); // Z�� ����

        Vector2 direction = screenPos - (Vector2)shootTransform.position;//���⺤��
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        sequence = DOTween.Sequence().SetUpdate(true)
       .Append(shoot.transform.DORotate(new Vector3(0, 0, angle), 0.01f).SetEase(Ease.Linear))
       .Append(shoot.transform.DOMove(screenPos, 0.5f))
       .Append(DOTween.To(() => 0f, x => { shoot.SetActive(false); }, 1f, 0f))
       .Join(DOTween.To(() => 0f, x => { explosion.transform.position = screenPos; }, 1f, 0f))
       .Append(DOTween.To(() => 0f, x => { explosion.SetActive(true); }, 1f, 0f));
    }

    private void RandomText()
    {
        text.text = dialogue[Random.Range(0,dialogue.Length)];
    }  
}
