using UnityEngine;

public class Reset : MonoBehaviour
{

    [SerializeField] private GameObject[] enemiesAndDoor;
    private Vector2[] initialPositions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (enemiesAndDoor.Length == 0 )
            return;
        initialPositions = new Vector2[enemiesAndDoor.Length];
        for (int i = 0; i < enemiesAndDoor.Length; i++)
        {
            initialPositions[i] = enemiesAndDoor[i].transform.position;
        }
    }

 
    public void ActivateRoom(bool status)
    {
        if (enemiesAndDoor.Length == 0)
            return;
        for (int i= 0;i< enemiesAndDoor.Length; i++)
        {
            if (enemiesAndDoor[i] != null)
                enemiesAndDoor[i].SetActive(status);
                enemiesAndDoor[i].transform.position = initialPositions[i];
        }
    }
}
