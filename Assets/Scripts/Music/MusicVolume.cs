using UnityEngine;
using UnityEngine.UI;

namespace Music
{
    public class MusicVolume : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceMusic;
        [SerializeField] private AudioSource _audioSourceSound;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;

        private void Start()
        {
            _musicSlider.value = _audioSourceMusic.volume;
            _soundSlider.value = _audioSourceSound.volume;
            _musicSlider.onValueChanged.AddListener(ChangeVolumeMusic);
            _soundSlider.onValueChanged.AddListener(ChangeVolume);
        }

        private void ChangeVolumeMusic(float value)
        {
            _audioSourceMusic.volume = value;
        }

        private void ChangeVolume(float value)
        {
            _audioSourceSound.volume = value;
        }
    }
}