using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SingleGameUI : MonoBehaviour
{
    [SerializeField] private Button stageSelectBtn;
    [SerializeField] private Button nextStageBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button[] mainBtn;


    private void Awake()
    {
        //�� ��ȣ Ȯ���ǰ� ����
        //stageSelectBtn.onClick.AddListener(() => SceneChange());
        //nextStageBtn.onClick.AddListener(() => SceneChange());
        //retryBtn.onClick.AddListener(() => SceneChange());
        //foreach(var btn in mainBtn)
        //    btn.onClick.AddListener(() => SceneChange());
        
    }

    private void SceneChange(int num)
    {
       SceneManager.LoadScene(num);
    }
  
}
