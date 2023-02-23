using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour {

    public enum AudioType {
        SFX_3D, SFX_2D, SoundTrack
    }

    [Header("Mixer:"), Space(5)]
    [SerializeField]
    private AudioMixer _mixer;
    public AudioMixer Mixer() { return _mixer; }

    [SerializeField]
    private AudioMixerGroup _sfx;
    [SerializeField]
    private AudioMixerGroup _soundtrak;

    [SerializeField]
    private string _SFXPath = "SFX/";
    [SerializeField]
    private string _SoundTrackPath = "Music/";

    // Diccionario de AudioClip
    private Container<AudioClip> _elements;
    private Container<AudioElement> _playingMusic;

    // Unity Awake
    void Awake() {
        _elements = new Container<AudioClip>("Audio/");
        _playingMusic = new Container<AudioElement>();
        _mixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
    }

    public AudioElement Play3DSFX(string file, Transform parent = null) {
        return PlaySFX(file, parent, AudioType.SFX_3D);
    }

    // Método 
    public AudioElement PlaySFX(string file, Transform parent = null, AudioType type = AudioType.SFX_2D) {
        AudioElement a = IPlay(type, (parent != null ? parent : uCore.GameManager.AudioContainer()), _elements.Get(_SFXPath + file), _sfx);
        a.gameObject.AddComponent<Destroyable>().destroyIn(a.Source.clip.length);
        return a;
    }

    public bool IsPlayingSoundtrack(string file) {
        if (_playingMusic.Exists(file)) {
            return _playingMusic.Get(file).Source.isPlaying;
        }
        return false;
    }

    public void StopSoundTrack() {
        foreach(AudioElement sound in _playingMusic.Elements) {
            sound.Stop();
        }
    }

    public AudioElement PlaySoundtrack(string file) {
        if (!_playingMusic.Exists(file)) {
            AudioElement a = IPlay(AudioType.SoundTrack, uCore.GameManager.AudioContainer(), _elements.Get(_SoundTrackPath + file), _soundtrak);
            _playingMusic.Add(file, a);
            return a;
        }
        return _playingMusic.Get(file);
    }

    private AudioElement IPlay(AudioType type, Transform parent, AudioClip clip, AudioMixerGroup mixer) {
        AudioElement audio = new UnityEngine.GameObject(clip.name).AddComponent<AudioElement>();
        audio.transform.SetParent(parent, false);
        audio.Source.clip = clip;
        audio.Source.outputAudioMixerGroup = mixer;
        switch (type) {
            case AudioType.SFX_3D:
                audio.with3D().rollOfType(AudioRolloffMode.Custom);
                break;
            case AudioType.SFX_2D:
                audio.with2D();
                break;
        }
        audio.Play();
        return audio;
    }

}
