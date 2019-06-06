using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minijuego4 : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] Transform toys; //Personajes
   
    public GameObject[] arrayPrefab;
    private int _i = 0, _dec = 0, _inc = 0;

    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HasChanged()
    {
        
            /*foreach (Transform slotTransform in slots)
            {
                GameObject item = slotTransform.GetComponent<SlotContent>().item;
                if (item)
                {
                    _iSlot = slotTransform.GetComponent<Slot>().id;
                }
            }*/
    }
    private void RandomPosition()
    {
        int index = Random.Range(0, arrayPrefab.Length);
        GameObject toy = null;
        switch (index)
        {
            case 0:
                Debug.Log("indice 0,sumar" + index);
                toy = arrayPrefab[index];
                arrayPrefab[index] = arrayPrefab[index + 1];
                arrayPrefab[index + 1] = toy;
                break;
            case 2:
                Debug.Log("Indice 2, restar" + index);
                toy = arrayPrefab[index];
                arrayPrefab[index] = arrayPrefab[index - 1];
                arrayPrefab[index - 1] = toy;
                break;
            default:
                Debug.Log("Indice 1, sumar o restar" + index);
                toy = arrayPrefab[index];
                arrayPrefab[index] = arrayPrefab[index - 1];
                arrayPrefab[index - 1] = toy;
                break;
        }
        SetToysSlots();
    }
    private void SetToysSlots(){
        foreach (Transform spriteTransform in toys)
        {
            int _j = GetIndex();
            GameObject objectHijo = Instantiate(arrayPrefab[_j]) as GameObject;
            objectHijo.name = arrayPrefab[_j].name;
            objectHijo.transform.SetParent(spriteTransform.transform);
            objectHijo.transform.position = spriteTransform.transform.position;
            if(objectHijo.name == "smugie_m" || objectHijo.name == "smugie_f")
                objectHijo.transform.localScale = spriteTransform.transform.localScale;
            else
                objectHijo.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            Debug.Log("Indice: " + _j);
        }
    }
    private int GetIndex(){
        if(_i == 0 && _inc == 1){
            _inc = 0;
            _i ++;
        }
        else if(_i >= 0 && _i < arrayPrefab.Length && _inc == 0 && _dec == 0){
            if(_i == (arrayPrefab.Length - 1)){
                _i --;
                _inc = 1;
                _dec = 1;
            }else
                _i ++;
        }
        else if(_i > 0 && _i < arrayPrefab.Length && _dec == 1 && _inc == 1){
            _i --;
            if(_i == 0){
                _dec = 0;
            }
        }
        else
            _i = 0;
        return _i;
    }
}
