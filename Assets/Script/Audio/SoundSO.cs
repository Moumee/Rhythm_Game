using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SoundScriptableObject")]
public class SoundSO : ScriptableObject
{
    public Sound[] bgmSounds, sfxSounds;
}
