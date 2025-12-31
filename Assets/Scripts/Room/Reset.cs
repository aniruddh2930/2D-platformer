using UnityEngine;

public class Reset : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector2[] initialPositions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPositions = new Vector2[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            initialPositions[i] = enemies[i].transform.position;
        }
    }

    // Update is called once per frame
    public void ActivateRoom(bool status)
    {
        for (int i= 0;i< enemies.Length; i++)
        {
            if (enemies[i] != null)
                enemies[i].SetActive(status);
                enemies[i].transform.position = initialPositions[i];
        }
    }
}
