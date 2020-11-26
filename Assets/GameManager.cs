using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


[System.Serializable]
public class Contenedor
{
    public List<Objeto> objetos;

    
}

[System.Serializable]
public class Objeto
{
    public GameObject objeto;
    public string nombre;
    public Vector3 posicion;

    public Objeto(string nombre,Vector3 posicion,GameObject objeto = null)
    {
        this.nombre = nombre;
        this.posicion = posicion;
        this.objeto = objeto;
    }
}

public enum Cara
{
    None,
    Arriba,
    Abajo,
    Este,
    Oeste,
    Norte,
    Sur
}

public class GameManager : MonoBehaviour

{
    public static GameManager singleton;
    public Vector3 dimensiones;
    public Contenedor objetos;
    public GameObject cubo;
    public bool canLoad;
    public GameObject[] prefabs;
    public int seleccionado;
    public int maxNiveles;
    public int salidas;
    public int salidasCompletadas;
    public Transform contenedorBotones;
    public Transform mapa;
    int nivelCargado;
    public GameObject resetBoton;


    // Start is called before the first frame update
    void Start()
    {
        singleton = this;

        if (!canLoad)
        {
            for (int x = 0; x < dimensiones.x; x++)
            {
                for (int y = 0; y < dimensiones.y; y++)
                {
                    for (int z = 0; z < dimensiones.z; z++)
                    {
                        GameObject obj = (GameObject)Instantiate(cubo, new Vector3(x, y, z), Quaternion.identity);
                        Crear(obj, new Vector3(x, y, z));
                    }
                }

            }
        }
        else
        {
            //Application.dataPath+"/Resources/archivo.json"
            for (int i = 0; i < 999; i++)

            {
                if (Resources.Load<TextAsset>("Nivel " + i) == null)
                {
                    maxNiveles = i;
                    break;
                }

            }
            for (int i = 0; i < maxNiveles -1; i++)
            {
                Instantiate(contenedorBotones.GetChild(0), contenedorBotones);

            }
            resetBoton.SetActive(false);

        }





    }

    // Update is called once per frame
    void Crear(GameObject obj, Vector3 posicion)
    {
        objetos.objetos.Add(new Objeto(obj.name.Replace("(Clone)", ""), posicion, obj));
    }

    public void Cargar(int nivel)
    {
        if (nivel<=maxNiveles)
        {
            var archivo = Resources.Load<TextAsset>("Nivel "+nivel);
            objetos = JsonUtility.FromJson<Contenedor>(archivo.text);

            if (objetos.objetos.Count>0)
            {
                nivelCargado = nivel;

                for (int i = 0; i < objetos.objetos.Count; i++)
                {
                    if (objetos.objetos[i].nombre.Contains("Salida"))
                    {
                        salidas++;

                    }

                    GameObject obj = (GameObject) Instantiate(Resources.Load<GameObject>("Prefabs/" + objetos.objetos[i].nombre), objetos.objetos[i].posicion, Quaternion.identity);
                    obj.transform.SetParent(mapa);
                }


                Camera.main.gameObject.GetComponent<CamMov>().enabled = true;
                //Camera.main.gameObject.GetComponent<CamMov>().player = GameObject.FindGameObjectWithTag("Player");
                contenedorBotones.gameObject.SetActive(false);
                resetBoton.SetActive(true);


            }
        }
    }

    public void BorrarMapa()
    {
        Camera.main.gameObject.GetComponent<CamMov>().enabled = false;
        for (int i = 0; i<mapa.childCount; i++)
        {
            Destroy(mapa.GetChild(i).gameObject);

            
        }

        salidas = 0;
        salidasCompletadas = 0;

    }

    public void CompletarSalida()
    {
        salidasCompletadas++;
        if (salidasCompletadas == salidas)
        {
            BorrarMapa();
            Cargar(nivelCargado + 1);
        } 
    }

    public void Guardar()

    {
        for (int i = 0; i < 999; i++)

        {
            if (Resources.Load<TextAsset>("Nivel " + i) == null)
            {
                maxNiveles = i;
                break;
            }

        }

        System.IO.File.WriteAllText(Application.dataPath+"/Resources/Nivel "+maxNiveles+".json",JsonUtility.ToJson(objetos));
        AssetDatabase.Refresh();


    }

    public void Restart()
    {

        BorrarMapa();
        Cargar(nivelCargado);

    }




    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                switch (GetHitFace(hit))
                {
                    case Cara.Arriba:
                        GameObject objArriba = (GameObject)Instantiate(prefabs[seleccionado], new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), Quaternion.identity);
                        Crear(objArriba, objArriba.transform.position);
                        break;
                    case Cara.Abajo:
                        GameObject objAbajo = (GameObject)Instantiate(prefabs[seleccionado], new Vector3(hit.transform.position.x, hit.transform.position.y - 1, hit.transform.position.z), Quaternion.identity);
                        Crear(objAbajo, objAbajo.transform.position);
                        break;
                    case Cara.Norte:
                        GameObject objNorte = (GameObject)Instantiate(prefabs[seleccionado], new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + 1), Quaternion.identity);
                        Crear(objNorte, objNorte.transform.position);
                        break;
                    case Cara.Sur:
                        GameObject objSur = (GameObject)Instantiate(prefabs[seleccionado], new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 1), Quaternion.identity);
                        Crear(objSur, objSur.transform.position);
                        break;
                    case Cara.Este:
                        GameObject objEste = (GameObject)Instantiate(prefabs[seleccionado], new Vector3(hit.transform.position.x + 1, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                        Crear(objEste, objEste.transform.position);
                        break;
                    case Cara.Oeste:
                        GameObject objOeste = (GameObject)Instantiate(prefabs[seleccionado], new Vector3(hit.transform.position.x - 1, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                        Crear(objOeste, objOeste.transform.position);
                        break;
                    default:
                        GameObject objdefault = (GameObject)Instantiate(prefabs[seleccionado], new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), Quaternion.identity);
                        Crear(objdefault, objdefault.transform.position);
                        break;

                }


            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i<objetos.objetos.Count; i++)
                {
                    if (objetos.objetos[i].objeto==hit.collider.gameObject)
                    {
                        Destroy(hit.collider.gameObject);
                        objetos.objetos.RemoveAt(i);
                        break;

                    }
                }
                
               
                

            }
        }
    }
    public void seleccionar (int seleccion)
    {
        seleccionado = seleccion;

    }

    public Cara GetHitFace(RaycastHit hit)
    {
        Vector3 incomingVec = hit.normal - Vector3.up;

        if (incomingVec == new Vector3(0, -1, -1))
            return Cara.Sur;

        if (incomingVec == new Vector3(0, -1, 1))
            return Cara.Norte;

        if (incomingVec == new Vector3(0, 0, 0))
            return Cara.Arriba;

        if (incomingVec == new Vector3(1, 1, 1))
            return Cara.Abajo;

        if (incomingVec == new Vector3(-1, -1, 0))
            return Cara.Oeste;

        if (incomingVec == new Vector3(1, -1, 0))
            return Cara.Este;   

        return Cara.None;
    }

}
