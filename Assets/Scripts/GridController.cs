using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private Sprite movementTileSprite = null;
    [SerializeField] private Sprite normalTileSprite = null;
    [SerializeField] private Sprite selectedTileSprite = null;
    [SerializeField] private Sprite attackableTileSprite = null;

    public Character SelectedCharacter { get; private set; } = null;
    public Sprite AvailabilitySprite => movementTileSprite;
    public Sprite NormalSprite => normalTileSprite;
    public Sprite SelectedSprite => selectedTileSprite;
    public Sprite AttackableTileSprite => attackableTileSprite;

    public int NbColumns { get; private set; } = 0;
    public int NbLines { get; private set; } = 0;
    
    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        NbColumns = (int)(GetComponent<RectTransform>().rect.width / gridLayoutGroup.cellSize.x);
        NbLines = (int)(GetComponent<RectTransform>().rect.height / gridLayoutGroup.cellSize.y);
    }

    public void SelectCharacter(Character character)
    {
        if(SelectedCharacter != null) DeselectCharacter();
        SelectedCharacter = character;
    }

    public void DeselectCharacter()
    {
        SelectedCharacter = null;
        foreach (Transform child in transform)
        {
            child.GetComponent<Tile>().HideTileAvailability();
        }
    }
    

    public Tile GetTile(int x, int y)
    {
        return transform.GetChild(x + y * NbColumns).GetComponent<Tile>();
    }
}
