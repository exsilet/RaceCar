using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private Image _select;
        
        private Vector3 _currentPosition;
        private bool _selected = false;

        public bool Selected => _selected;

        public void Select()
        {
            _selected = true;
            _currentPosition = transform.position;
        }

        public void Deselect()
        {
            _selected = false;
        }

        public void CurrentPosition()
        {
            transform.position = _currentPosition;
        }
    }
}