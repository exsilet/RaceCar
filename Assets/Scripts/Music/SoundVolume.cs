using UnityEngine;

namespace Music
{
    public class SoundVolume : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceSound;
        [SerializeField] private AudioClip _clickAudio;
        [SerializeField] private AudioClip _crossingCarAudio;
        [SerializeField] private AudioClip _ShopCarAudio;
        [SerializeField] private AudioClip _BoxCarAudio;
        [SerializeField] private AudioClip _newCarAudio;
        [SerializeField] private AudioClip _closeAudio;

        public void CloseSound()
        {
            //_audioSourceSound.volume = PlayerPrefs.GetFloat(SoundVolume);
            _audioSourceSound.clip = _closeAudio;
            _audioSourceSound.Play();
        }

        public void ClickSound()
        {
            //_audioSourceSound.volume = PlayerPrefs.GetFloat(SoundVolume);
            _audioSourceSound.clip = _clickAudio;
            _audioSourceSound.Play();
        }
        
        public void ShopCarSound()
        {
            //_audioSourceSound.volume = PlayerPrefs.GetFloat(SoundVolume);
            _audioSourceSound.clip = _ShopCarAudio;
            _audioSourceSound.Play();
        }
        
        public void BoxCarSound()
        {
            //_audioSourceSound.volume = PlayerPrefs.GetFloat(SoundVolume);
            _audioSourceSound.clip = _BoxCarAudio;
            _audioSourceSound.Play();
        }
        
        public void CrossingCarSound()
        {
            //_audioSourceSound.volume = PlayerPrefs.GetFloat(SoundVolume);
            _audioSourceSound.clip = _crossingCarAudio;
            _audioSourceSound.Play();
        }
        
        public void NewCarSound()
        {
            //_audioSourceSound.volume = PlayerPrefs.GetFloat(SoundVolume);
            _audioSourceSound.clip = _newCarAudio;
            _audioSourceSound.Play();
        }
    }
}