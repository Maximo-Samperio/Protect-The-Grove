using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public Queue<GameObject> Enemies = new Queue<GameObject>();    // Queue with enemies
    public GameObject[] _array;                    // I put all GO in an array
    private int _head;                              // Head of the array
    private int _tail;                              // Tail of the array
    private int _size;                              // Size of the Q
    public int enemyToAdd;


    public GameObject enemy;
    public int count;
    public float rate;

    //public void Enqueue (GameObject newItem)        // Method to put enemies inside the Q
    //{
    //    _array[_tail] = newItem;                    // I pass the item
    //    _tail = (_tail + 1) % _array.Length;        // Modular operator so that tail does not go out of bounds
    //    _size++;                                    // I increase the size of the Q
    //}

    //public void Dequeue(GameObject item)                           // Method to remove enemies from Q (opposite to Enqueue)
    //{
    //    GameObject element = _array[_head];
    //    _head = (_head + 1) % _array.Length;
    //    _size--;
    //    return;
    //}
}
