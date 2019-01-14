using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAudio : MonoBehaviour {
	public AudioSource source;
	public Dictionary<string, List<AudioClip>> library;

	void Start() {
		this.source = GetComponent<AudioSource>();
		library = new Dictionary<string, List<AudioClip>>();
		UpdateAudioSources();
	}

	public void UpdateAudioSources() {
		library.Clear();
		foreach(AudioStorage storage in GetComponentsInChildren<AudioStorage>()) {
			library.Add(storage.audioType, storage.audioClips);
		}
	}
}