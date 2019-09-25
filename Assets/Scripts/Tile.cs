using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

public abstract class Tile : MonoBehaviour
{
    private Button button;
    private readonly TileType tileType;
    private Image image;
    private Character character;
    private GridController gridController;
    private bool avalaibiltyDisplayed = false;
    
    public bool IsWalkable => tileType != TileType.OBSTACLE;
    public bool IsAvailable => IsWalkable && character == null;
    private Vector3 position;
    public Vector3 Position => transform.position;

    protected Tile(TileType tileType)
    {
        this.tileType = tileType;
    }
    
    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(OnCellClick); 
        gridController = transform.parent.GetComponent<GridController>();
    }

    protected void Start()
    {
        int index = transform.GetSiblingIndex();
        int nbColumns = Utils.Finder.GridController.NbColumns;
        position.x = index % nbColumns;
        position.y = index / nbColumns;
    }

    private void OnCellClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (gridController.SelectedCharacter != null && avalaibiltyDisplayed && character == null)
        {
            Finder.GridController.HideTilesAvailability();
            gridController.SelectedCharacter.MoveTo(this);
            gridController.SelectCharacter(null);
        }
        if(character == null) return;
        if (character == gridController.SelectedCharacter)
        {
            gridController.HideTilesAvailability();
            gridController.DeselectCharacter();
            return;
        }
        DisplayTileSelected();
        gridController.SelectCharacter(character);
        for (int i = -character.Range; i <= character.Range; i++)
        {
            for(int j = -character.Range; j <= character.Range ; j++)
            {
                if (i != 0 || j != 0)
                {
                    if (position.x + i >= 0 && position.y + j >= 0 && position.x + i < gridController.NbColumns && position.y + j < gridController.NbColumns)
                    {
                        if (Math.Abs(i) + Math.Abs(j) <= character.Range)
                        {
                            Tile tile = gridController.GetTile((int)position.x + i, j + (int)position.y);
                            if (tile.IsAvailable)
                            {
                                tile.DisplayTileAvailability();
                            }
                        }
                    }
                }
            }
        }
    }

    private void DisplayTileAvailability()
    {
        avalaibiltyDisplayed = true;
        image.sprite = gridController.AvailabilitySprite;
    }
    
    public void HideTileAvailability()
    {
        avalaibiltyDisplayed = false;
        image.sprite = gridController.NormalSprite;
    }

    public void DisplayTileSelected()
    {
        image.sprite = gridController.SelectedSprite;
    }
    
    public bool LinkCharacter(Character character)
    {
        if (!IsWalkable) return false;
        this.character = character;
        Debug.Log(character.gameObject.name + " linked to " + gameObject.name);
        return character != null;
    }

    public bool UnlinkCharacter()
    {
        if (character == null) return false;
        Debug.Log(character.gameObject.name + " unlinked from " + gameObject.name);
        character = null;
        return true;
    }

    public Character GetCharacter()
    {
        return character;
    }
}

public enum TileType{
    EMPTY = 0,
    OBSTACLE = 1,
    FOREST = 2,
    FORTRESS = 3,
    DOOR = 4
}

