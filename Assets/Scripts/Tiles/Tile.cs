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
    private bool AvalaibiltyDisplayed => image.sprite = gridController.AvailabilitySprite;
    
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
        var selectedCharacter = gridController.SelectedCharacter;
        
        // Si un personnage est sélectionné
        if (character != null)
        {
            // Si aucun personnage n'était sélectionné
            if (selectedCharacter == null)
            {
                if (character is Ally && character.IsPlayable)
                {
                    gridController.SelectCharacter(character);
                    DisplayPossibleActions();
                }
            } 
            else
            {
                if (character is Enemy && gridController.SelectedCharacter.IsPlayable)
                {
                    // Attaque un ennemi
                    if (AvalaibiltyDisplayed)
                    {
                        gridController.SelectedCharacter.Attack(character);
                        if (character.Died())
                        {
                            character.Die();
                            gridController.SelectedCharacter.MoveTo(this);
                        }
                        gridController.DeselectCharacter();
                    }
                    else
                    {
                        gridController.DeselectCharacter();
                    }
                }
                else if (selectedCharacter == character)
                {
                    // Sélectionne l'unité déjà sélectionné
                    gridController.DeselectCharacter();
                }
                else
                {
                    // Sélectionne une autre unité
                    gridController.SelectCharacter(character);
                    DisplayPossibleActions();
                }
            }
        }
        else
        {
            if (selectedCharacter != null && AvalaibiltyDisplayed)
            {
                selectedCharacter.MoveTo(this);
            }
            gridController.DeselectCharacter();
        }
    }

    private void DisplayPossibleActions()
    {
        image.sprite = gridController.SelectedSprite;
        for (int i = -character.Range; i <= character.Range; i++)
        {
            for(int j = -character.Range; j <= character.Range ; j++)
            {
                if (i != 0 || j != 0)
                {
                    if (position.x + i >= 0 && position.y + j >= 0 && position.x + i < gridController.NbColumns && position.y + j < gridController.NbLines)
                    {
                        if (Math.Abs(i) + Math.Abs(j) <= character.Range)
                        {
                            Tile tile = gridController.GetTile((int)position.x + i, j + (int)position.y);
                            if (tile.IsAvailable)
                            {
                                tile.DisplayTileAvailability();
                            }
                            else
                            {
                                if (tile.character is Enemy)
                                {
                                    tile.DisplayTileAttackable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    

    private void DisplayTileAvailability()
    {
        image.sprite = gridController.AvailabilitySprite;
    }

    private void DisplayTileAttackable()
    {
        image.sprite = gridController.AttackableTileSprite;
    }
    
    public void HideTileAvailability()
    {
        image.sprite = gridController.NormalSprite;
    }

    public bool LinkCharacter(Character character)
    {
        if (!IsWalkable) return false;
        this.character = character;
        return character != null;
    }

    public bool UnlinkCharacter()
    {
        if (character == null) return false;
        character = null;
        return true;
    }
}

public enum TileType 
{
    EMPTY = 0,
    OBSTACLE = 1,
    FOREST = 2,
    FORTRESS = 3,
    DOOR = 4
}

