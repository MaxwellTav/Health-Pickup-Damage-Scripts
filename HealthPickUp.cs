using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    //varibles
    [Tooltip("Esta seran las referencias a los scripts, si da error, agregala manualmente")]
    [Header("Referencia al script")]
    public HealthPlayerManager _healthPlayerManager;
    public AudioDB _audioDB;

    [Header("____________________________________")]
    [Header("Configuracion de sumatoria de vidas")]
    [Tooltip("Esta cantidad sera la que se le sumará a la vida")]
    public float _HealthPlus = 30;

    [Header("____________________________________")]
    [Header("Detallitos y algo más...")]
    public GameObject GO_Mesh;
    [Tooltip("imposibilitar que vuelva a coger (mmm... que rico, coger) el objeto si no esta disponible")]
    public bool Available = true;
    public float _timeToReturnToNormal = 60;
    public AudioSource _audioSource;


    /*
     * 
     * SpetialClip Hints
     
         El SC (SpetialClip) 2 = El de cuando aparece despues de cogerlo
         El SC 3 = El de cuando coges el objeto
     *
     *
    */



    // Start is called before the first frame update
    void Start()
    {
        Available = true;
        _audioDB = GameObject.FindObjectOfType<AudioDB>();
        //_audioSource = GetComponent<AudioSource>();
        StartCoroutine(Wait4GetComponents());
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("Player")) && (_healthPlayerManager.HealthAmount < _healthPlayerManager.HealthDataAmount) && (Available == true))
        {
            _healthPlayerManager.HealthAmount += _HealthPlus;
            Debug.Log("El pana se curó");

            _audioSource.clip = _audioDB.SpecialClip[3];
            _audioSource.Play();

            LockEvent();
        }
        else if (other.gameObject.CompareTag("Player") && _healthPlayerManager.HealthAmount == _healthPlayerManager.HealthDataAmount)
        {
            Debug.Log("Tiene la vida llena");
        }
    }
    
    public void LockEvent()
    {
        //ya no podra coger nuevamente el vainita ese...
        GO_Mesh.SetActive(false);
        Available = false;
        StartCoroutine(ReturToNormal());
    }

    //Coorutinas

    IEnumerator Wait4GetComponents()
    {
        yield return new WaitForSeconds(1);
        _healthPlayerManager = FindObjectOfType<HealthPlayerManager>();
    }

    IEnumerator ReturToNormal()
    {
        yield return new WaitForSeconds(_timeToReturnToNormal);
        Available = true;
        GO_Mesh.SetActive(true);
        _audioSource.clip = _audioDB.SpecialClip[2];
        _audioSource.Play();
        Debug.Log("Se completó el proceso correctamente");
    }
}
