using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private GameObject _point;
    [SerializeField] private float _launchForce;
    [SerializeField] private int _numberOfPoints;
    [SerializeField] private float _spaceBetweenPoints;

    private GameObject[] _points;
    private Vector2 direction;

    private void Start()
    {
        _points = new GameObject[_numberOfPoints];

        for (int i = 0; i < _numberOfPoints; i++)
        {
            _points[i] = Instantiate(_point, _shotPoint.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = direction;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        for (int i = 0; i < _numberOfPoints; i++)
        {
            _points[i].transform.position = PointPosition(i * _spaceBetweenPoints);
        }
    }

    private void Shoot()
    {
        GameObject newArrow = Instantiate(_arrow, _shotPoint.position, _shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * _launchForce;
    }

    private Vector2 PointPosition(float time)
    {
        Vector2 position = (Vector2)_shotPoint.position + (direction.normalized * _launchForce * time) + 0.5f * Physics2D.gravity * (time * time);
        return position;
    }
}
