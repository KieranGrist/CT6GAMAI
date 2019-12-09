using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    public GameObject FinishLine, Sector1, Sector2;
    [Header("Prefabs")]
    public GameObject StraightPrefab;
    public GameObject CornerPrefab;
    public GameObject PitPrefab;
    public GameObject GridPrefab;
    public GameObject FinishPrefab;
    public List<GameObject> gameObjects = new List<GameObject>();
    public static RaceTrack raceTrack;
    public bool GenMap;
    GameObject PreviousPiece;
    int Id;

    // Start is called before the first frame update

    public void GenerateTrack()
    {
        foreach (var item in gameObjects)
            Destroy(item);
        gameObjects.Clear();

        Id = 1;
        GameObject GO = Instantiate(GridPrefab, transform);
        GO.transform.position = new Vector3(0, 0, 0);
        gameObjects.Add(GO);
        GO.name = Id.ToString(); Id++;


        GO = Instantiate(FinishPrefab, transform);
        GO.transform.position = new Vector3(0, 0, 40);
        gameObjects.Add(GO);
        GO.name = Id.ToString(); Id++;

        GO = Instantiate(StraightPrefab, transform);
        GO.transform.position = new Vector3(0, 0, -40);
        gameObjects.Add(GO);
        GO.name = Id.ToString(); Id++;

        GO = Instantiate(PitPrefab, transform);
        GO.transform.position = new Vector3(20, 0, 0);
        gameObjects.Add(GO);
        GO.name = Id.ToString(); Id++;



        GO = Instantiate(StraightPrefab, transform);
        GO.transform.position = new Vector3(0, 0, 120);
        gameObjects.Add(GO);
        GO.name = Id.ToString(); Id++;
        PreviousPiece = GO;


        //Random Loop
        //Choose Piece
        //Direction of piece comes from the piece before it so if the piece is going to the west the direction would be east as direction is based on where it came from not going to.
        for (int i = 0; i < 15; i++)
        {
            var TrackPiece = Random.value >= .5f; // True = Straight, false = corner;

            if (TrackPiece)
            {
                GenStraight();
            }
            else
            {
                GenCorner();
            }
        }

    }
    void GenStraight()
    {
        var GO = Instantiate(StraightPrefab, transform);
        GO.name = Id.ToString(); Id++;
        gameObjects.Add(GO);
        if (PreviousPiece.GetComponent<Straight>())
            if (PreviousPiece.GetComponent<Straight>())
            {
                GO.GetComponent<Straight>().roadDirection = PreviousPiece.GetComponent<Straight>().roadDirection;
                switch (PreviousPiece.GetComponent<Straight>().roadDirection)
                {
                    case RoadDirection.North:
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(0, 0, 40);
                        break;
                    case RoadDirection.East:
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(40, 0, 0);
                        break;

                    case RoadDirection.South:
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(0, 0, 40);
                        break;

                    case RoadDirection.West:
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(40, 0, 0);
                        break;

                }
            }
        if (PreviousPiece.GetComponent<Corner>())
            switch (PreviousPiece.GetComponent<Corner>().Direction)
            {
                case CornerDirection.EastToNorth:
                case CornerDirection.WestToNorth:
                    GO.GetComponent<Straight>().roadDirection = RoadDirection.North;
                    GO.transform.position = PreviousPiece.transform.position + new Vector3(0, 0, 30);
                    break;
                case CornerDirection.WestToSouth:
                case CornerDirection.EastToSouth:
                    GO.GetComponent<Straight>().roadDirection = RoadDirection.South;
                    GO.transform.position = PreviousPiece.transform.position - new Vector3(0, 0, 30);
                    break;

                case CornerDirection.SouthToEast:
                case CornerDirection.NorthToEast:
                    GO.GetComponent<Straight>().roadDirection = RoadDirection.East;
                    GO.transform.position = PreviousPiece.transform.position + new Vector3(30, 0, 0);
                    break;
                case CornerDirection.SouthToWest:
                case CornerDirection.NorthToWest:
                    GO.GetComponent<Straight>().roadDirection = RoadDirection.West;
                    GO.transform.position = PreviousPiece.transform.position - new Vector3(30, 0, 0);
                    break;
            }
        Debug.Log(GO.name + " Collision Log");
        foreach (var item in Physics.OverlapBox(GO.transform.position, GO.transform.localScale * .5f,GO.transform.rotation, LayerMask.GetMask("Road")))
        {
            Debug.Log(item.name);
            Debug.Log(item.transform.parent.name);
            if (item.transform.parent.gameObject != GO || item.gameObject != GO)
            { 
                    Debug.Log(GO.name + " Collision Log");
                    Debug.Log(item.name);
                    Debug.Log(item.transform.parent.name);
                    Debug.Log("Collided");
                
            }
        
        }



        PreviousPiece = GO;

    }
    void GenCorner()
    {
        var GO = Instantiate(CornerPrefab, transform);
        GO.name = Id.ToString(); Id++;

        gameObjects.Add(GO);
        if (PreviousPiece.GetComponent<Straight>())
            switch (PreviousPiece.GetComponent<Straight>().roadDirection)
            {
                case RoadDirection.North:
                    var D = Random.value >= .5f;
                    if (D)
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.SouthToEast;
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(0, 0, 30);
                    }
                    else
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.SouthToWest;
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(0, 0, 30);
                    }
                    break;

                case RoadDirection.East:
                    D = Random.value >= .5f;
                    if (D)
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.WestToNorth;
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(30, 0, 0);
                    }
                    else
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.WestToSouth;
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(30, 0, 0);
                    }
                    break;


                case RoadDirection.South:
                    D = Random.value >= .5f;
                    if (D)
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.NorthToEast;
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(0, 0, 30);
                    }
                    else
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.NorthToWest;
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(0, 0, 30);
                    }
                    break;


                case RoadDirection.West:
                    D = Random.value >= .5f;
                    if (D)
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.EastToNorth;
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(30, 0, 0);
                    }
                    else
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.EastToSouth;
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(30, 0, 0);
                    }
                    break;

            }
        if (PreviousPiece.GetComponent<Corner>())
            switch (PreviousPiece.GetComponent<Corner>().Direction)
            {
                case CornerDirection.NorthToEast:
                case CornerDirection.SouthToEast:
                    var D = Random.value >= .5f;
                    if (D)
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.WestToNorth;
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(20, 0, 0);
                    }
                    else
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.WestToSouth;
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(20, 0, 0);
                    }
                    break;

                case CornerDirection.NorthToWest:
                case CornerDirection.SouthToWest:
                    D = Random.value >= .5f;
                    if (D)
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.EastToNorth;
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(20, 0, 0);
                    }
                    else
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.EastToSouth;
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(20, 0, 0);
                    }
                    break;
                case CornerDirection.EastToNorth:
                case CornerDirection.WestToNorth:
                    D = Random.value >= .5f;
                    if (D)
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.SouthToEast;
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(0, 0, 20);
                    }
                    else
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.SouthToWest;
                        GO.transform.position = PreviousPiece.transform.position + new Vector3(0, 0, 20);
                    }
                    break;
                case CornerDirection.EastToSouth:
                case CornerDirection.WestToSouth:
                    D = Random.value >= .5f;
                    if (D)
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.NorthToEast;
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(0, 0, 20);
                    }
                    else
                    {
                        GO.GetComponent<Corner>().Direction = CornerDirection.NorthToWest;
                        GO.transform.position = PreviousPiece.transform.position - new Vector3(0, 0, 20);
                    }
                    break;


            }
        foreach (var item in Physics.OverlapBox(GO.transform.position, GO.transform.localScale * .5f, GO.transform.rotation, LayerMask.GetMask("Road")))
        {
      
            if (item.transform.parent.gameObject != GO || item.gameObject != GO )
            {
                Debug.Log(GO.name + " Collision Log");
                Debug.Log(item.name);
                Debug.Log(item.transform.parent.name);
                Debug.Log("Collided");
            }
        }

    PreviousPiece = GO;

    }
    public void GenerateObstacles()
    {

    }
    private void Awake()
    {
        raceTrack = this;



    }
    // Update is called once per frame
    void Update()
    {
        raceTrack = this;
        if (GenMap)
        {
            GenMap = false;
            GenerateTrack();
        }
    }
}