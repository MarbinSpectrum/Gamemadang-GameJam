using UnityEngine;

public class SoundObj : MonoBehaviour
{
    public Sound sound;

    public void PlaySound()
    {
        SoundMgr.Instance.PlaySE(sound);
    }
}
