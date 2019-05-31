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
    public Sprite arrow, uIMask;
    public int _iArrow = 4, _iSlot = 4, _iTurn = 0, _iMov = 1, _iArrowPrev = 0;
    public float currentTime = 0;
	float maxTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        CheckBall();
        HasChanged();
    }

    // Update is called once per frame
    void Update()
    {
        VerifiedSlot();
        HasChanged();
    }

    private void RandomPositionArrow()
    {
        _iArrowPrev = _iArrow;
        _iArrow = Random.Range(0, 4);

        if(_iArrowPrev != 4 && _iArrowPrev != 4){
            emojiArrow[_iArrowPrev].GetComponent<Image>().sprite = uIMask;
        }

        if(_iArrowPrev != _iArrow){
            emojiArrow[_iArrow].GetComponent<Image>().sprite = arrow;
            Debug.Log("posicion: "+ _iArrow);
        }else if(_iArrow == 3){
            _iArrow -= 1;
            emojiArrow[_iArrow].GetComponent<Image>().sprite = arrow;
            Debug.Log("posicion: "+ _iArrow);
        }else{
            _iArrow +=1;
            emojiArrow[_iArrow].GetComponent<Image>().sprite = arrow;
            Debug.Log("posicion: "+ _iArrow);
        }
    }

    //Obtiene el valor del sprite posicionado en el rostro de emojis
    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
   
        if(_iTurn == 0){
            _iArrow = 4;
            _iSlot = 4;
            _iTurn += 1;
            RandomPositionArrow();
        }
        else if(_iTurn != 0){
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
    }

    private void CheckBall(){
        if(_iArrow == _iSlot){
            Debug.Log("slot: "+ _iSlot + " posicion: "+ _iArrow + " turno " + _iTurn);
        }
        else{
            Debug.Log("No slot: "+ _iSlot + " posicion: "+ _iArrow + " turno " + _iTurn);
        }
    }

    private void VerifiedSlot()
	{
		currentTime += Time.deltaTime;
		
		if(currentTime >= maxTime){
			currentTime = 0;
			if(_iArrow == _iSlot && _iTurn != 0)
			{
                if(_iTurn == 1){
                    Debug.Log("1. slot: "+ _iSlot + " posicion: "+ _iArrow + " turno " + _iTurn);
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 2)
                {
                    Debug.Log("1. slot: "+ _iSlot + " posicion: "+ _iArrow + " turno " + _iTurn);
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 3)
                {
                    Debug.Log("1. slot: "+ _iSlot + " posicion: "+ _iArrow + " turno " + _iTurn);
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 4)
                {
                    Debug.Log("1. slot: "+ _iSlot + " posicion: "+ _iArrow + " turno " + _iTurn);
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 5)
                {
                    Debug.Log("1. slot: "+ _iSlot + " posicion: "+ _iArrow + " turno " + _iTurn);
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 6)
                {
                    Debug.Log("1. slot: "+ _iSlot + " posicion: "+ _iArrow + " turno " + _iTurn);
                    RandomPositionArrow();
                    _iTurn += 1;
                }else if(_iTurn == 7)
                {
                    Debug.Log("1. slot: "+ _iSlot + " posicion: "+ _iArrow + " turno " + _iTurn);
                }
            }
	    }
    }

}


