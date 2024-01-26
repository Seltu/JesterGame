using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevationMecanic : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _lava;
    [SerializeField] private float __camSpeed;
    [SerializeField] private float __lavaSpeed;

    private bool _isElevating;
    
    private void Start()
    {
        StartCoroutine(SetElevationTrue());
    }

    private void FixedUpdate()
    {
        if(_isElevating)
            MoveUp();
    }           

    private void MoveUp()
    {
        Vector2 elevationCamMovement = new Vector2(0, 0);
        Vector2 elevationLavaMovement = new Vector2(0, 0);

        elevationCamMovement.y = __camSpeed * Time.deltaTime;    
        elevationLavaMovement.y = __lavaSpeed * Time.deltaTime;    

        _cam.transform.position += (Vector3)elevationCamMovement;
        _lava.position += (Vector3)elevationLavaMovement;
    }

    private IEnumerator SetElevationTrue()
    {
        yield return new WaitForSeconds(3f);

        _isElevating = true;
    }
}
