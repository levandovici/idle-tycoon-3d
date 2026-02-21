using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private bool _selected = false;

    [SerializeField]
    private bool _move = false;



    public bool Selected
    {
        get
        {
            return _selected;
        }

        set
        {
            if(_selected != value)
            {
                _move = true;
            }

            _selected = value;
        }
    }



    private void Update()
    {
        if (!_move)
            return;

        Vector3 target;

        if (_selected)
        {
            target = new Vector3(transform.position.x, 10f, transform.position.z);
        }
        else
        {
            target = new Vector3(transform.position.x, 0f, transform.position.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, (_selected ? _speed : _speed * _speed * 0.25f) * Time.deltaTime);

        if(Vector3.Distance(transform.position, target) < 0.01f)
        {
            _move = false;
        }
    }



    public void Select()
    {
        Selected = true;
    }

    public void Unselect()
    {
        Selected = false;
    }
}
