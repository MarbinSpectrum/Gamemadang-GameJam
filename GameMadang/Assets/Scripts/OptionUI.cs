using UnityEngine;
using UnityEngine.UI;
public class OptionUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void OnEnable()
    {
        float soundValue = SoundMgr.Instance.GetVolume();
        slider.value = soundValue;

        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener((v) =>
        {
            SoundMgr.Instance.SetVolume(v);
        });
    }
}
