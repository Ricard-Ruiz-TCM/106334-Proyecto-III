using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    [SerializeField, Header("Mixer:")]
    private AudioMixer _mixer;
    [SerializeField]
    private AudioMixerGroup _sfx;
    [SerializeField]
    private AudioMixerGroup _soundtrak;
    public AudioMixer Mixer() {
        return _mixer;
    }

    [SerializeField, Header("Folder Paths:")]
    private string _AudioPath = "Audio/";
    [SerializeField]
    private string _SFXPath = "SFX/";
    [SerializeField]
    private string _SoundtrackPath = "Music/";
    [SerializeField]
    private string _MixerPath = "Settings/AudioMixer";

    // Diccionario de AudioClip & AudioElements
    private Container<AudioClip> _audios;
    private Container<AudioElement> _playing;

    // Unity Awake
    void Awake() {
        _playing = new Container<AudioElement>();
        _audios = new Container<AudioClip>(_AudioPath);
        _mixer = Resources.Load<AudioMixer>(_MixerPath);
        _sfx = _mixer.FindMatchingGroups("SFX")[0];
        _soundtrak = _mixer.FindMatchingGroups("Soundtrack")[0];
    }

    // * ------------ *
    // | - Play SFX - |
    // V ------------ V
    public AudioElement PlaySFX(string file, audioType type = audioType.SFX_2D) {
        return PlaySFX(file, null, type);
    }
    public AudioElement PlaySFX(string file, Vector3 position, audioType type = audioType.SFX_3D) {
        return PlaySFX(file, type).setPosition(position);
    }
    public AudioElement PlaySFX(string file, Transform parent, audioType type = audioType.SFX_3D) {
        return IPlay(type, _audios.Get(_SFXPath + file), _sfx).setParent(parent).destroyAtEnd();
    }
    // A ------------ A

    // * ------------------ *
    // | - Play Sountrack - |
    // V ------------------ V
    public void StopAllSoundtracks() {
        foreach (AudioElement st in _playing.Elements) {
            st.Stop();
        }
    }
    public void StopSoundtrack(string file) {
        if (IsPlayingSoundtarck(file)) {
            _playing.Get(file).Stop();
        }
    }
    public bool IsPlayingSoundtarck(string file) {
        if (!_playing.Exists(file)) {
            return false;
        }
        return _playing.Get(file).Source.isPlaying;
    }
    public AudioElement PlaySoundtrack(string file) {
        if (IsPlayingSoundtarck(file)) {
            return _playing.Get(file).Reset();
        }
        return _playing.Add(file, IPlay(audioType.SoundTrack, _audios.Get(_SoundtrackPath + file), _soundtrak));
    }
    // A ------------------ A

    /** Método IPlay
     * Crea, configura y reproduce un audio concreto, añadiendo todo lo necesario
     * para su funcionamiento
     * @param audioType type Tipo de audio
     * @param AudioClip cilp Clip de audio a reproducir
     * @param AudioMixerGroup mixer Grupo del mixer donde se reproducida y controlara volumen y efectos
     * @return AudioElement el objeto creado
     */
    private AudioElement IPlay(audioType type, AudioClip clip, AudioMixerGroup mixer) {
        AudioElement audio = new GameObject(clip.name).AddComponent<AudioElement>();
        audio.Source.clip = clip;
        audio.Source.outputAudioMixerGroup = mixer;
        switch (type) {
            case audioType.SFX_3D:
                audio.with3D().rollOfType(AudioRolloffMode.Custom);
                break;
            case audioType.SFX_2D:
                audio.with2D();
                break;
            case audioType.SoundTrack:
                audio.with2D();
                break;
            default:
                break;
        }
        audio.Play();
        return audio;
    }

}
