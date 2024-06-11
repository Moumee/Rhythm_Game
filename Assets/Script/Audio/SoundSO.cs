using UnityEngine;




[CreateAssetMenu(menuName = "ScriptableObject/SoundScriptableObject")]
public class SoundSO : ScriptableObject
{
    public SFXAudioClip[] sfxAudioClipArray;
    public BGMAudioClip[] bgmAudioClipArray;

    [System.Serializable]
    public class SFXAudioClip
    {
        public AudioManager.SFX sfx;
        public AudioClip audioClip;
    }

    [System.Serializable]
    public class BGMAudioClip
    {
        public AudioManager.BGM bgm;
        public AudioClip audioClip;
    }
}
