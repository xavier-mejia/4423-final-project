using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionTarget : MonoBehaviour
{
    private Collider2D _collider;
    private AudioSource _audioSource;
    [SerializeField] public GameObject music;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().isPaused = true;
            music.GetComponent<AudioSource>().Pause();
            AudioListener.pause = false;
            StartCoroutine(CompleteLevel());
        }
    }

    IEnumerator CompleteLevel()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}
