using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayerManager : MonoBehaviour
{
    //Variables
    [Header("Configuracion de Salúd")]
    [Tooltip("Esta sera la cantidad interna para la salud")]
    public float HealthAmount = 200f;
    [HideInInspector]
    public float HealthDataAmount;
    [Tooltip("Este sera el objeto que mostrará en pantalla la salud")]
    public Text HealthText;
    
    private bool HasLifeYet = true;

    [Header("Padre a eliminar")]
    [Header("")]
    [Tooltip("Esta es la referencia del padre de los objetos a eliminar")]
    public GameObject PadreGO;
    [Tooltip("Este sera el tiempo que durara el GO (GameObject) para ser eliminado")]
    public float EraseDelayTime = 5;


    // Start is called before the first frame update
    void Start()
    {
        //inicializando las cosas.
        HasLifeYet = true;
        HealthDataAmount = HealthAmount;

        //cubriendo errores
        if(HealthText == null)
        {
            Debug.Log("No has asignado el componente: HeatlhText");
        }
    }

    void Update()
    {
        //esto hace que se muestre la vida
        HealthText.GetComponent<Text>().text = HealthAmount.ToString();



        //que no pase de rango-------------------------------------------------------------
        if (HealthAmount <= 0)                                                          //-
        {                                                                               //-
            HealthAmount = 0;                                                           //-
        }                                                                               //-
                                                                                        //-
        if (HealthAmount >= 200)                                                        //-
        {                                                                               //-
            HealthAmount = 200;                                                         //-
        }                                                                               //-
        //que no pase de rango-------------------------------------------------------------


        //Condiciones--------------------------------------------------------------------------------------------------------
        if(HealthAmount <= 0)
        {
            HasLifeYet = false;
            Destroy(PadreGO, EraseDelayTime);
            Debug.Log("Ha muerto el jugador: " + gameObject.name);
        }




    }
}
