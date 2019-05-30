using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minijuego5 : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] Text elementText;

    public Button[] emojiArrow;
    public Sprite arrow;
    public int _iArrow, _iSlot;

    // Start is called before the first frame update
    void Start()
    {
        RandomPositionArrow();
        HasChanged();
        CheckBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomPositionArrow(){
        _iArrow = Random.Range(0, 4);
        emojiArrow[_iArrow].GetComponent<Image>().sprite = arrow;
        Debug.Log("posicion: "+ _iArrow);
    }

    //Obtiene el valor del sprite posicionado en el rostro de emojis
    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
   
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotContent>().item;
            if (item)
            {
                builder.Append(item.name);
                _iSlot = slotTransform.GetComponent<Slot>().id;
                Debug.Log("slot: "+ _iSlot);
            }
        }
        elementText.text = builder.ToString();
    }

    private void CheckBall(){
        if(_iArrow == _iSlot){
            Debug.Log("slot: "+ _iSlot + " posicion: "+ _iArrow);
        }
        else{
            Debug.Log("No slot: "+ _iSlot + " posicion: "+ _iArrow);
        }
    }
}


