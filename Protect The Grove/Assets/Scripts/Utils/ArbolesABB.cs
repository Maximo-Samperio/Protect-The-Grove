using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using UnityEngine;


public class BinarySearchTreeNode
{
    
    public int info;
    public GameObject go;
    public BinaryTree leftChild;
    public BinaryTree rightChild;
}

public class BinaryTree
{
    public BinarySearchTreeNode root;

    public int Root()
    {
        return root.info;
    }

    public bool IsTreeEmpty()
    {
        return (root == null);
    }

    public void InitializeTree()
    {
        root = null;
    }

    public BinaryTree RightChild()
    {
        return root.rightChild;
    }

    public BinaryTree LeftChild()
    {
        return root.leftChild;
    }

    public void AddElement(int x, GameObject go)
    {
        if (root == null)
        {
            root = new NodoABB();
            root.info = x;
            root.go = go;
            root.leftChild = new ABB();
            root.leftChild.InitializeTree();
            root.rightChild = new ABB();
            root.rightChild.InitializeTree();
        }
        else if (root.info > x)
        {
            root.leftChild.AddElement(x, go);
        }
        else if (root.info < x)
        {
            root.rightChild.AddElement(x, go);
        }
    }

    public void DeleteElement(int x)
    {
        if (root != null)
        {
            if (root.info == x && root.leftChild.IsTreeEmpty() && root.rightChild.IsTreeEmpty())
            {
                root = null;
            }
            else if (root.info == x && !root.leftChild.IsTreeEmpty())
            {
                root.info = this.FindLargest(root.leftChild);
                root.leftChild.DeleteElement(root.info);
            }
            else if (root.info == x && root.leftChild.IsTreeEmpty())
            {
                root.info = this.FindSmallest(root.rightChild);
                root.rightChild.DeleteElement(root.info);
            }
            else if (root.info < x)
            {
                root.rightChild.DeleteElement(x);
            }
            else
            {
                root.leftChild.DeleteElement(x);
            }
        }
    }

    public int FindLargest(BinaryTree a)
    {
        if (a.RightChild().IsTreeEmpty())
        {
            return a.Root();
        }
        else
        {
            return FindLargest(a.RightChild());
        }
    }

    public int FindSmallest(BinaryTree a)
    {
        if (a.LeftChild().IsTreeEmpty())
        {
            return a.Root();
        }
        else
        {
            return FindSmallest(a.LeftChild());
        }
    }
}


