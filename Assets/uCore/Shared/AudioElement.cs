using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioElement : MonoBehaviour {

    private void Awake() {
        Source.playOnAwake = true;
    }

    public AudioSource Source { get { return GetComponent<AudioSource>(); } }

    public void Play() {
        Source.Play();
    }

    public void Stop() {
        Source.Pause();
    }

    public void FadeOut(float time) {
        StartCoroutine(C_FadeOut(time));
    }

    private IEnumerator C_FadeOut(float time) {
        yield return new WaitForSeconds(time);
        if (Source.volume >= 0.01f) {
            Source.volume -= 0.1f * Time.deltaTime;
            StartCoroutine(C_FadeOut(time));
        }
        Stop();
        yield return null;
    }

    public void PlayDelayed(float delay) {
        Source.PlayDelayed(delay);
    }

    public void PlayScheduled(double time) {
        Source.PlayScheduled(time);
    }

    public void Mute() {
        Source.mute = true;
    }

    public void UnMute() {
        Source.mute = false;
    }

    public void EnableByPassEffects() {
        Source.bypassEffects = true;
    }

    public void DisableByPassEffects() {
        Source.bypassEffects = false;
    }

    public AudioElement looped() {
        Source.loop = true;
        return this;
    }

    public void destroyoAtEnd() {
        gameObject.AddComponent<Destroyable>().destroyIn(Source.clip.length - Source.time);
    }

    public void destroyOnTime(float time) {
        gameObject.AddComponent<Destroyable>().destroyIn(time);
    }

    public AudioElement noLoop() {
        Source.loop = false;
        return this;
    }

    public AudioElement withVolumeEq(float volume) {
        Source.volume = volume;
        return this;
    }

    public AudioElement withPitchEq(float value) {
        Source.pitch = value;
        return this;
    }

    public AudioElement withPanningEq(float pan) {
        Source.panStereo = pan;
        return this;
    }

    public AudioElement with3D(float value = 1.0f) {
        Source.spatialBlend = value;
        return this;
    }

    public AudioElement with2D(float value = 0.0f) {
        with3D(value);
        return this;
    }

    public AudioElement onMinMaxDistance(float min = 0.0f, float max = 12.0f) {
        Source.maxDistance = max;
        Source.minDistance = min;
        return this;
    }

    public AudioElement rollOfType(AudioRolloffMode type, float min = 0.0f, float max = 12.0f) {
        Source.rolloffMode = type;
        onMinMaxDistance(min, max);
        return this;
    }

    public AudioElement randomPitch(float min, float max) {
        return withPitchEq(Random.Range(min, max));
    }

    public AudioElement randomVolume(float min, float max) {
        return withVolumeEq(Random.Range(min, max));
    }


}
