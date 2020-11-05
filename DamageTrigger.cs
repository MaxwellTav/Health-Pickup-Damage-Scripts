using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    //variables
    private HealthPlayerManager _healthPlayerManager;
    [HideInInspector]
    public BoxCollider _boxCollider;
    [HideInInspector]
    public MeshRenderer _meshRenderer;

    [Header("Daño")]
    [Tooltip("Este será el daño que causará el collider al player")]
    public float Damage = 10;
    
    [Header("Debug")]
    [Tooltip("Si esta encendida esta opcion, el \"Mesh Renderer\" de este objeto sera visible en partida")]
    public bool _debug;


    private void Start()
    {
        StartCoroutine(DelayEvent());

        //Debug
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;

        _meshRenderer = GetComponent<MeshRenderer>();
        
        if (_debug)
        {
            _meshRenderer.enabled = true;
            Debug.Log("Está debugeando el damage");
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _healthPlayerManager.HealthAmount = _healthPlayerManager.HealthAmount - Damage;
            if (_debug)
                Debug.Log("Se hizó: " + Damage + " de daño");
        }
    }

    IEnumerator DelayEvent()
    {
        yield return new WaitForSeconds(1);
        _healthPlayerManager = FindObjectOfType<HealthPlayerManager>();
    }
}
