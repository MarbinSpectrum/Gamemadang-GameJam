using UnityEngine;
using UnityEngine.UI;

public class SingleGameUI : MonoBehaviour
{
    [SerializeField] private Button stopBtn;

    private void Awake()
    {
        stopBtn.onClick.AddListener(() => StopGame());
    }

    private void StopGame()
    {
        Time.timeScale = Time.timeScale==1f ? 0f : 1f;
    }
}
