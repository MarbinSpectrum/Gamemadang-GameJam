using System.Collections.Generic;
using UnityEngine;


public class SoundMgr : Singleton<SoundMgr>
{
    private Dictionary<Sound, AudioClip> sounds = new Dictionary<Sound, AudioClip>();
    [SerializeField] private AudioSource se;
    [SerializeField] private AudioSource bgm;
    [Range(0, 1)] public float soundValue = 0.5f;

    protected override void Awake()
    {
        base.Awake();

        SoundResourceLoad();
    }

    private void SoundResourceLoad()
    {
        string loadFormat = "Sound/{0}";
        KeyValuePair<Sound, string>[] kvpArr = new KeyValuePair<Sound, string>[]
        {
            new KeyValuePair<Sound, string>(Sound.SE_WrongExplosion,"wrongexplosion"),
            new KeyValuePair<Sound, string>(Sound.SE_LaserShooting,"lasershooting"),
            new KeyValuePair<Sound, string>(Sound.SE_GameEndPopup,"gameendpopup"),
            new KeyValuePair<Sound, string>(Sound.SE_Explosion,"explosion"),
            new KeyValuePair<Sound, string>(Sound.SE_CutScene,"cutscene"),
            new KeyValuePair<Sound, string>(Sound.SE_Click,"click"),

            new KeyValuePair<Sound, string>(Sound.BGM_DarkFight,"darkfightbgm"),
        };

        foreach (KeyValuePair<Sound, string> pair in kvpArr)
        {
            string route = string.Format(loadFormat, pair.Value);
            AudioClip audio = Resources.Load(route, typeof(AudioClip)) as AudioClip;
            sounds.Add(pair.Key, audio);
        }
    }

    public void PlaySE(Sound pSound)
    {
        if (sounds.ContainsKey(pSound) == false)
            return;
        AudioClip clip = sounds[pSound];
        se.volume = soundValue;
        se.PlayOneShot(clip);
    }

    public void PlayBGM(Sound pSound)
    {
        if (sounds.ContainsKey(pSound) == false)
            return;
        AudioClip clip = sounds[pSound];
        bgm.Stop();
        bgm.volume = soundValue;
        bgm.clip = clip;
        bgm.Play();
    }

    public void SetVolume(float v)
    {
        soundValue = v;
        bgm.volume = v;
    }

    public float GetVolume() => soundValue;
}
