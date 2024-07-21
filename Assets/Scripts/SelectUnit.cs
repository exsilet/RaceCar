using System.Collections.Generic;
using Music;
using UnityEngine;

namespace DefaultNamespace
{
    public class SelectUnit : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Spawn _spawn;
        [SerializeField] private SoundVolume _sound;

        private Camera _camera;
        private RaycastHit _currentHit;
        private Transform _selected;
        private Vector3 _offset;
        private int _nextLevel;
        private Selectable _currentSelectable;
        private GameObject _selectedObject;
        private GarageSlot _currentGarageSlot;
        private GarageSlot _selectGarageSlot;

        private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

        private void Awake() => _camera = Camera.main;

        private void LateUpdate()
        {
//#if UNITY_EDITOR

            if (Input.GetMouseButtonDown(0))
                SingleSelect();

            if (Input.GetMouseButtonUp(0))
                UnSelected();
//#endif

            // if (Input.touchCount > 0)
            // {
            //     var touch = Input.GetTouch(0);
            //
            //     if (touch.phase == TouchPhase.Began)
            //         SingleSelect();
            //
            //     if (touch.phase == TouchPhase.Ended)
            //         UnSelected();
            // }

            DragObject();
        }

        private void SingleSelect()
        {
            Vector2 clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (Physics.Raycast(TouchRay, out var hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Selectable>())
                    {
                        _currentSelectable = hit.collider.gameObject.GetComponent<Selectable>();
                        _currentSelectable.Select();
                        _selected = hit.transform;
                        _currentGarageSlot = hit.transform.parent.GetComponent<GarageSlot>();
                        _selectedObject = hit.collider.gameObject;
                        _selectedObject.GetComponent<Collider>().enabled = false;
                    }
                }
            }
        }

        private void DragObject()
        {
            if (_selectedObject == null)
                return;

            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                _camera.WorldToScreenPoint(_selectedObject.transform.position).z);
            Vector3 worldPosition = _camera.ScreenToWorldPoint(position);

            _selectedObject.transform.position = new Vector3(worldPosition.x, 0.3f, worldPosition.z);
        }

        private void UnSelected()
        {
            if (_selected != null)
            {
                if (Physics.Raycast(TouchRay, out var hit))
                {
                    Debug.Log("hit: " + hit.collider.name);

                    if (hit.collider.TryGetComponent(out Selectable select))
                    {
                        Car car = select.GetComponent<Car>();
                        
                        if (car.Level == _selectedObject.GetComponent<Car>().CarStaticData.Level)
                        {
                            _sound.CrossingCarSound();
                            NewCarLevel(car);
                        }
                    }
                }

                ReturnToCurrentPosition();
            }
        }

        private void NewCarLevel(Car car)
        {
            _nextLevel = car.Level;
            _nextLevel++;
            _selectGarageSlot = car.transform.parent.GetComponent<GarageSlot>();
            _spawn.SpawnCar(_nextLevel, _selectGarageSlot);
            _currentGarageSlot.IsGarage();
            _currentGarageSlot.DestroyMinions(_selectedObject);
            _selectGarageSlot.DestroyMinions(car.gameObject);
            _selectGarageSlot.IsGarage();
        }

        private void ReturnToCurrentPosition()
        {
            _selectedObject.GetComponent<Collider>().enabled = true;
            _selectedObject = null;
            _currentSelectable.Deselect();
            _currentSelectable.CurrentPosition();
            _currentSelectable = null;
            _selected = null;
        }
    }
}