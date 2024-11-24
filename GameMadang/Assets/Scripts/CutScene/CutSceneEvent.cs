using DG.Tweening;
using TMPro;
using UnityEngine;

public class CutSceneEvent : MonoBehaviour
{
    //넣을 수도 있는거 효과음, 움직임, 대사변환 정도?

    [SerializeField]private TextMeshProUGUI text;
    private string[] dialogue = { "받아라 정상화의 총알!", "거기 너! 숨어봤자 소용없다!","이제 그만 너희 세계로 돌아가!","이세계 정상화!" };

    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private GameObject shootVFX;

    [SerializeField] private Transform shootTransform;
    [SerializeField] private GameObject image;

    private GameObject throwObj;

    GameObject shoot;
    GameObject explosion;

    Sequence sequence;

    SoundObj sound;

    public void StartCutScene()
    {
        Destroy(shoot);
        Destroy(explosion);

        throwObj.SetActive(false);
        sound.sound = Sound.SE_CutScene;
        sound.PlaySound();
        RandomText();
        image.SetActive(true);

        image.transform.DOLocalMoveX(0,0.1f).SetUpdate(true);


    }
    public void ThrowObj()
    {
        Sequence objSequence = DOTween.Sequence().SetUpdate(true)
        .Append(throwObj.transform.DOMove(new Vector2(10,10), 1f))
        .Join(throwObj.transform.DORotate(new Vector3(0, 0, 360), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1));
    }

    public void EndCutScene()
    {
        gameObject.SetActive(false);
    }

    //이펙트를 하나로 합치고 
    public void ShootSFX()
    {
        Vector2 mousePosition = GameManager.Instance.clickPosition;
        GameObject throwGameObj = GameManager.Instance.hitOtherObj;
        ShootSFX(mousePosition, throwGameObj);
    }

    public void ShootSFX(Vector2 screenPos, GameObject pThrowGameObj)
    {
        throwObj = pThrowGameObj;
        throwObj.GetComponent<SpriteRenderer>().sortingOrder = 100;

        throwObj.transform.localScale = new Vector3(throwObj.transform.localScale.x, throwObj.transform.localScale.y, throwObj.transform.localScale.z) * 2;

        sound = gameObject.GetComponent<SoundObj>();
        sound.sound = Sound.SE_LaserShooting;
        sound.PlaySound();

        Time.timeScale = 0f;

        shoot = Instantiate(shootVFX, shootTransform);//SFX 생성
        explosion = Instantiate(explosionVFX, shootTransform);//SFX 생성

        explosion.SetActive(false);

        shoot.transform.position = new Vector3(shoot.transform.position.x, shoot.transform.position.y, 0); // Z축 고정

        Vector2 direction = screenPos - (Vector2)shootTransform.position;//방향벡터
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
