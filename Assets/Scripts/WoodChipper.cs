using UnityEngine;
using DG.Tweening;

public class WoodChipper : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransformOne;
    [SerializeField]
    private Transform _targetTransformTwo;
    [SerializeField]
    private ParticleSystem _bloodParticles;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _mixerClip;
    [SerializeField]
    private AudioClip _splashClip;

    private Lemming _lemming;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Lemming>(out Lemming lemming))
        {
            _lemming = lemming;
            Suck();
        }
    }

    private void Suck()
    {
        _lemming.transform.DOMove(_targetTransformOne.position, 0.2f).OnComplete(KillLemming);
    }

    private void KillLemming()
    {
        _lemming.Dead();
         PlayMixerSound();
        
        Debug.Log("show blood");
    }

    private void PlayMixerSound()
    {
        _audioSource.PlayOneShot(_mixerClip);
        Invoke("ShowBlood", 1);
    }

    private void ShowBlood()
    {
        PlaySplashSound();
        _bloodParticles.Play();
    }

    private void PlaySplashSound() 
    {
        _audioSource.PlayOneShot(_splashClip);
    }
}
