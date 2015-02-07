using UnityEngine;
using System.Collections;

public class MoodClas : MonoBehaviour {

    private Vector3 vectorIni; // Posicion inicial- tambien como color inicial
    private Vector3 vectorFin; // Posicion final - tambien usado como color final
    private Vector4 vector4Aux; //vector auxiliar cast Vector3 to Vector4 usado para color
    public float speed; // velocidad cambio
    private float distancia; //variable para calcular 
    const int SAD = 1;
    const int HAPPY = 2;
    const int ANGRY = 3;
    const int SCARED = 4;
    const int AWARE = 0; //Mood intermedio de transición
    public int estado; // El estado de animo entre 0 y 4, otros valores por defaul corresponden a 0 o Aware
    
    void Start()
    {
        vectorIni = new Vector3(1, 1, 0); //Inicia en estado sorpresa o Aware
        
        CalcularDistancia(); //llama la funcion calcular distancia necesaria para el calculo de velocidad
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado) { //Revisa cual es el siguiente estado de animo
        
            case SAD:
                vectorFin= new Vector3(0.5F,0.5F,0.5F); //color gris
                break;
            case HAPPY:
                vectorFin=new Vector3(0.0F,1.0F,0.5F); //Color Verde
                break;
            case ANGRY:
                vectorFin=new Vector3(1.0F,0.0F,0.0F); //Color Rojo
                break;
            case SCARED:
                vectorFin=new Vector3(0.5F,0.0F,1.0F); //Color Morado
                break;
            default:
                vectorFin=new Vector3(1.0F,1.0F,0.0F); //Color amarillo AWARE
                break;
        }
        
        HacerSlearp();
        
        vectorIni = gameObject.transform.position; //Reasigno posicion ultima posición al vector inicial

    }

    //La funcion HacersLearp realiza la interpolación de posición y Color del gameObject usando Vector3.Slerp
    public void HacerSlearp()
    {
        
        transform.position = Vector3.Slerp(vectorIni, vectorFin, Time.deltaTime * speed); //Cambio de Posición interpolada
        Vector3 vectorAux = Vector3.Slerp(vectorIni, vectorFin, Time.deltaTime * speed); //Almaceno en un vector3 auxiliar
        vector4Aux = vectorAux; //Convierto el vector3 a vector4 para usar con material.color
        vector4Aux.w = 1; //asigno al valor de transparencia 1=255, para que se vea el color
        gameObject.renderer.material.color = vector4Aux; //Hago el cambio de Color
    }

    //La función Calcular distancia, calcula la distancia entre los vectores
    //Inicial y final para definir la velocidad de transformacion
    //No esta implementada actualmente porque en el update el ultimo valor que 
    //devuelve es la distancia con la ultima posición al terminar el lerp es decir cero.
    public void CalcularDistancia() {
        distancia = Vector3.Distance(vectorIni, vectorFin);
        Debug.Log("Distancia = " + distancia);
    
    }
}
