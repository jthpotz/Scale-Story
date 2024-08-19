using System;
using System.Collections.Generic;
using Godot;

public partial class AudioQueue : AudioStreamPlayer {

    private Queue<AudioStream> queue = new Queue<AudioStream>();

    public enum AudioType {
            mp3,

            wav
        }

    public override void _Ready () {
        Finished += OnFinished;
    }

    public void QueueSound(string path, AudioType format) {
        if (format == AudioType.mp3) {
            queue.Enqueue(GD.Load<AudioStreamMP3>(path));
        }
        else if (format == AudioType.wav) {
            queue.Enqueue(GD.Load<AudioStreamWav>(path));
        }
        if (!Playing) {
            OnFinished();
        }
    }

    public void Play(string path, AudioType format) {
        if (format == AudioType.mp3) {
            Stream = GD.Load<AudioStreamMP3>(path);
        }
        else if (format == AudioType.wav) {
            Stream = GD.Load<AudioStreamWav>(path);
        }
        Play();
    }

    public void OnFinished () {
        if (queue.Count > 0) {
            Stream = queue.Dequeue();
            Play();
        }
    }

    public void ClearQueue () {
        queue.Clear();
    }

}
