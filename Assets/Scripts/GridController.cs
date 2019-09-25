using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private Sprite availabilitySprite = null;
    [SerializeField] private Sprite normalSprite = null;
    [SerializeField] private Sprite selectedSprite = null;

    public Character SelectedCharacter { get; private set; } = null;
    public Sprite AvailabilitySprite => availabilitySprite;
    public Sprite NormalSprite => normalSprite;
    public Sprite SelectedSprite => selectedSprite;

    public int NbColumns { get; private set; } = 0;
    
    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        NbColumns = (int)(GetComponent<RectTransform>().rect.width / gridLayoutGroup.cellSize.x);
    }

    public void SelectCharacter(Character character)
    {
        SelectedCharacter = character;
    }

    public void DeselectCharacter()
    {
        SelectedCharacter = null;
    }

    public void HideTilesAvailability()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Tile>().HideTileAvailability();
        }
    }

    public Tile GetTile(int x, int y)
    {
        Tile tile = transform.GetChild(x + y * NbColumns).GetComponent<Tile>();
        return tile;
    }
}
