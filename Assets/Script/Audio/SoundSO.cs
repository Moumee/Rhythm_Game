using FMODUnity;
using UnityEngine;




[CreateAssetMenu(menuName = "ScriptableObject/SoundScriptableObject")]
public class SoundSO : ScriptableObject
{
    public SFXAudioClip[] sfxAudioClipArray;
    public BGMAudioClip[] bgmAudioClipArray;
    public StageAudioClip[] stageAudioClipArray;

    [System.Serializable]
    public class SFXAudioClip
    {
        public AudioManager.SFX sfx;
        public EventReference sfxSound;
    }

    [System.Serializable]
    public class BGMAudioClip
    {
        public AudioManager.BGM bgm;
        public EventReference bgmSound;
    }

    [System.Serializable]
    public class StageAudioClip
    {
        public AudioManager.Stage stage;
        public EventReference stageSound; 
    }

    public EventReference clickSound;
    public EventReference startSound;

}
