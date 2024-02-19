using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuManager : MonoBehaviour
{
    public GameObject[] Panel;
    // Start is called before the first frame update
    void Start()
    {
        // Set Panel 1 to false and Panel 0 to true
        Panel[1].SetActive(false);
        Panel[0].SetActive(true);
    }
    // OnChangePanel function to switch between panels 
    public void OnChangePanel(int index)
    {
        // If index is 1, set Panel 0 to true and Panel 1 to false 
        if(index == 1)
        {
            Panel[0].SetActive(true);
            Panel[1].SetActive(false);
        } 
        // If index is 2, set Panel 1 to true and Panel 0 to false 
        if(index == 2)
        {
            Panel[1].SetActive(true);
            Panel[0].SetActive(false);
        } 
    }
}
