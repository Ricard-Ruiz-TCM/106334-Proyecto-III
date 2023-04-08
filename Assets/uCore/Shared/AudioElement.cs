using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioElement : BasicElement<AudioElement> {

    public AudioSource Source { get { return GetComponent<AudioSource>(); } }

    public AudioElement Reset() {
        Stop(); Play(Source.volume);
        return this;
    }

    public AudioElement Play(float volume = -1f) {
        Source.volume = (volume == -1f ? Source.volume : volume);
        Source.Play();
        return this;
    }

    public AudioElement Stop() {
        Source.Pause();
        return this;
    }

    public AudioElement FadeOut(float time, float delay = 0f, float min = 0f) {
        if (Source.isPlaying) {
            StartCoroutine(C_FadeAudio(time, delay, Source.volume, min));
        }
        return this;
    }

    public AudioElement FadeIn(float time, float delay = 0f, float max = 1f) {
        if (!Source.isPlaying) {
            Play(0f);
        }
        StartCoroutine(C_FadeAudio(time, delay, Source.volume, max));
        return this;
    }

    private IEnumerator C_FadeAudio(float time, float delay, float start, float end) {
        yield return new WaitForSeconds(delay);
        float startTime = Time.time;
        while (Time.time < startTime + time) {
            Source.volume = Mathf.Lerp(start, end, (Time.time - startTime) / time);
            yield return null;
        }
        Source.volume = end;
    }

    public AudioElement PlayDelayed(float delay) {
        Source.PlayDelayed(delay);
        return this;
    }

    public AudioElement PlayScheduled(double time) {
        Source.PlayScheduled(time);
        return this;
    }

    public AudioElement Mute() {
        Source.mute = true;
        return this;
    }

    public AudioElement UnMute() {
        Source.mute = false;
        return this;
    }

    public AudioElement EnableByPassEffects() {
        Source.bypassEffects = true;
        return this;
    }

    public AudioElement DisableByPassEffects() {
        Source.bypassEffects = false;
        return this;
    }

    public AudioElement looped() {
        Source.loop = true;
        return this;
    }

    public override AudioElement destroyoAtEnd() {
        destroyOnTime(Source.clip.length - Source.time);
        return this;
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

    public AudioElement with3D(float value = 1f) {
        Source.spatialBlend = value;
        return this;
    }

    public AudioElement with2D(float value = 0f) {
        with3D(value);
        return this;
    }

    public AudioElement onMinMaxDistance(float min = 0f, float max = 12f) {
        Source.maxDistance = max;
        Source.minDistance = min;
        return this;
    }

    public AudioElement rollOfType(AudioRolloffMode type, float min = 0f, float max = 12f) {
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
