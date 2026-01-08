using UnityEngine;

public class Reset : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector2[] initialPositions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (enemies.Length == 1 && enemies[0]==null)
            return;
        initialPositions = new Vector2[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            initialPositions[i] = enemies[i].transform.position;
        }
    }

 
    public void ActivateRoom(bool status)
    {
        if (enemies.Length == 1 && enemies[0] == null)
            return;
        for (int i= 0;i< enemies.Length; i++)
        {
            if (enemies[i] != null)
                enemies[i].SetActive(status);
                enemies[i].transform.position = initialPositions[i];
        }
    }
}
