using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTile : MonoBehaviour
{

    [SerializeField]
    private GameObject eastTile;

    [SerializeField]
    private GameObject southTile;


    private void Awake()
    {
        eastTile.SetActive(false);
        southTile.SetActive(false);
    }

    public void ShowEast() => eastTile.SetActive(true);
    public void ShowSouth() => southTile.SetActive(true);

}
