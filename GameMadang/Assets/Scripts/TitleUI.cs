using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button gameExitBtn;
    [SerializeField] private Button singleGameBtn;
    [SerializeField] private Button multiGameBtn;

    private void Awake()
    {
        gameExitBtn.onClick.AddListener(() => GameExit());
        singleGameBtn.onClick.AddListener(() => SeneChange("StageSelect"));
    }
    private void GameExit()
    {
        Application.Quit();
    }

    private void SeneChange(string name)
    {
        SceneManager.LoadScene(name);
    }
  
}
