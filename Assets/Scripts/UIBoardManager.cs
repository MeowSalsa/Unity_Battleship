using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIBoardManager : MonoBehaviour
{
    public delegate void ChangeShip(int id, int size);
    public static event ChangeShip OnChangeShip;

    public delegate void ChangeShipOrientation();
    public static event ChangeShipOrientation OnChangeShipOrientation;

    public List<Button> collectionOfShipButtons = new List<Button>(5);
    public Button OrientationButton;
    private bool horizontal = false;

    //Horizontal + Vertical textures
    [SerializeField]
    private Sprite HorizontalTexture;
    [SerializeField]
    private Sprite VerticalTexture;

    Dictionary<int, int> ships = new Dictionary<int, int>() {
        {0,5}, //Aircraft Carrier
        {1,4}, //Battleship
        {2,3}, //Submarine
        {3,3}, //Destroyer
        {4,2}, //Patrol Boat
    };

    private void OnEnable()
    {
        BoardManager.OnBoardPiecePlaced += OnBoardPiecePlaced;
    }
    private void OnDisable()
    {
        BoardManager.OnBoardPiecePlaced -= OnBoardPiecePlaced;

    }

    private void Start()
    {
        UpdateOrientationUI();  
    }

    private void OnBoardPiecePlaced(int shipID)
    {
        Debug.Log("Disactivating Button Number " + shipID);
        collectionOfShipButtons[shipID].gameObject.SetActive(false);
        if (shipID < 4)
        {
            NextShip(shipID);
        }
    }
    private void NextShip(int shipID)
    {
        int newShipID = shipID + 1;
        OnChangeShip?.Invoke(newShipID, ships[newShipID]);
        Debug.Log("Invoking");
    }
    public void OnShipButtonClick(int id)
    {
        Debug.Log("ID " + id);
        OnChangeShip?.Invoke(id, ships[id]);
        string idString = id.ToString();
        Debug.Log("Pressed button " + idString);
    }

    public void OnOrientationClick()
    {
        Debug.Log("Rotation Floatation Motation!");
        UpdateOrientationUI();
        OnChangeShipOrientation?.Invoke();

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnOrientationClick();
            Debug.Log("Rotating Ship! W keypress");
        }
    }
    void UpdateOrientationUI()
    {
        Debug.Log("Initial Rotation " + horizontal);
        horizontal = !horizontal;
        Debug.Log("New rotation " + horizontal);
        if (horizontal)
        {
            Debug.Log("Horizontal Sprite");
            OrientationButton.image.sprite = HorizontalTexture;
        }
        else
        {
            Debug.Log("Vertical Sprite");
            OrientationButton.image.sprite = VerticalTexture;
        }
    }
}
