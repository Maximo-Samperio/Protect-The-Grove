using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public List<Turret> turrets;

    void Start()
    {
        SortTurretsByCost();
  
    }

    void SortTurretsByCost()
    {
        Quicksort(turrets.ToArray(), 0, turrets.Count - 1);
    }

    void Quicksort(Turret[] turrets, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(turrets, left, right);

            Quicksort(turrets, left, pivotIndex - 1);
            Quicksort(turrets, pivotIndex + 1, right);
        }
    }

    int Partition(Turret[] turrets, int left, int right)
    {
        int pivotIndex = left + (right - left) / 2;
        Turret pivotValue = turrets[pivotIndex];
        int i = left - 1;
        int j = right + 1;

        while (true)
        {
            do
            {
                i++;
            } while (turrets[i].Cost < pivotValue.Cost);

            do
            {
                j--;
            } while (turrets[j].Cost > pivotValue.Cost);

            if (i >= j)
                return j;

            Swap(turrets, i, j);
        }
    }

    void Swap(Turret[] turrets, int i, int j)
    {
        Turret temp = turrets[i];
        turrets[i] = turrets[j];
        turrets[j] = temp;
    }

}
