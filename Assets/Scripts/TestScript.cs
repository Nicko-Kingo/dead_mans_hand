using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    Deck deck;
    // Start is called before the first frame update
    void Start()
    {
        deck = new Deck();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(deck.Draw());
        }
    }
}
