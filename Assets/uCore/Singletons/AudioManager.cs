using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour {

    public enum AudioType {
        SFX_3D, SFX_2D, SoundTrack
    }

    // Mixer
    private AudioMixer _mixer;
    private AudioMixerGroup _sfx;
    private AudioMixerGroup _soundtrak;
    public AudioMixer Mixer() { return _mixer; }

    // Audio Settings Path
    private string _SFXPath = "SFX/";
    private string _AudioPath = "Audio/";
    private string _SoundTrackPath = "Music/";
    private string _MixerPath = "Settings/AudioMixer";

    // Diccionario de AudioClip
    private Container<AudioClip> _audios;
    private Container<AudioElement> _playing;

    // Unity Awake
    void Awake() {
        _playing = new Container<AudioElement>();
        _audios = new Container<AudioClip>(_AudioPath);
        _mixer = Resources.Load<AudioMixer>(_MixerPath);
    }

    public AudioElement PlaySFX(string file, AudioType type = AudioType.SFX_2D) {
        return PlaySFX(file, null, type);
    }
    public AudioElement PlaySFX(string file, Vector3 position, AudioType type = AudioType.SFX_3D) {
        return PlaySFX(file, type).setPosition(position);
    }
    public AudioElement PlaySFX(string file, Transform parent, AudioType type = AudioType.SFX_3D) {
        return IPlay(type, _audios.Get(_SFXPath + file), _sfx).setParent(parent).destroyoAtEnd();
    }

    public AudioElement PlaySoundTrack(string file) {
        if (!_playing.Exists(file)) {
            return _playing.Add(file, IPlay(AudioType.SoundTrack, _audios.Get(_SoundTrackPath + file), _soundtrak));
        }
        return _playing.Get(file);
    }

    private AudioElement IPlay(AudioType type, AudioClip clip, AudioMixerGroup mixer) {
        AudioElement audio = new GameObject(clip.name).AddComponent<AudioElement>();
        audio.Source.clip = clip;
        audio.Source.outputAudioMixerGroup = mixer;
        switch (type) {
            case AudioType.SFX_3D:
                audio.with3D().rollOfType(AudioRolloffMode.Custom);
                break;
            case AudioType.SFX_2D:
                audio.with2D();
                break;
            case AudioType.SoundTrack:
                audio.with2D();
                break;
            default: break;
        }
        audio.Play();
        return audio;
    }

}
