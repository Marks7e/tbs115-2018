using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minijuego5 : MonoBehaviour
{

    public Button[] emojiArrow;
    public Sprite arrow;
    public int _iArrow;

    // Start is called before the first frame update
    void Start()
    {
        RandomPositionArrow();
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

}
