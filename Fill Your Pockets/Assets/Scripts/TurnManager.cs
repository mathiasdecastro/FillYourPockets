using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int maxTurns = 10;
    public bool isGameOver = false;

    private int currentTurn = 0;

    public void EndTurn()
    {
        if (isGameOver)
            return;

        currentTurn++;

        if (currentTurn >= maxTurns)
        {
            isGameOver = true;
            Debug.Log("La partie est finie");
        }
        else
            Debug.Log("Tour : " + currentTurn + " Terminé.");
    }
}
