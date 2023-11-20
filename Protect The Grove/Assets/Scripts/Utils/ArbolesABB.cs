using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using UnityEngine;


public class NodoABB
{
    // datos a almacenar, en este caso un entero
    public int info;
    public GameObject go;
    // referencia los nodos izquiero y derecho
    public ABB hijoIzq;
    public ABB hijoDer;
}

public class ABB
{
    public NodoABB raiz;

    public int Raiz()
    {
        return raiz.info;
    }

    public bool ArbolVacio()
    {
        return (raiz == null);
    }

    public void InicializarArbol()
    {
        raiz = null;
    }

    public ABB HijoDer()
    {
        return raiz.hijoDer;
    }

    public ABB HijoIzq()
    {
        return raiz.hijoIzq;
    }

    public void AgregarElem(int x, GameObject go)
    {
        if (raiz == null)
        {
            raiz = new NodoABB();
            raiz.info = x;
            raiz.go = go;
            raiz.hijoIzq = new ABB();
            raiz.hijoIzq.InicializarArbol();
            raiz.hijoDer = new ABB();
            raiz.hijoDer.InicializarArbol();
        }
        else if (raiz.info > x)
        {
            raiz.hijoIzq.AgregarElem(x, go);
        }
        else if (raiz.info < x)
        {
            raiz.hijoDer.AgregarElem(x, go);
        }
    }

    public void EliminarElem(int x)
    {
        if (raiz != null)
        {
            if (raiz.info == x && raiz.hijoIzq.ArbolVacio() && raiz.hijoDer.ArbolVacio())
            {
                raiz = null;
            }
            else if (raiz.info == x && !raiz.hijoIzq.ArbolVacio())
            {
                raiz.info = this.mayor(raiz.hijoIzq);
                raiz.hijoIzq.EliminarElem(raiz.info);
            }
            else if (raiz.info == x && raiz.hijoIzq.ArbolVacio())
            {
                raiz.info = this.menor(raiz.hijoDer);
                raiz.hijoDer.EliminarElem(raiz.info);
            }
            else if (raiz.info < x)
            {
                raiz.hijoDer.EliminarElem(x);
            }
            else
            {
                raiz.hijoIzq.EliminarElem(x);
            }
        }
    }

    public int mayor(ABB a)
    {
        if (a.HijoDer().ArbolVacio())
        {
            return a.Raiz();
        }
        else
        {
            return mayor(a.HijoDer());
        }
    }

    public int menor(ABB a)
    {
        if (a.HijoIzq().ArbolVacio())
        {
            return a.Raiz();
        }
        else
        {
            return menor(a.HijoIzq());
        }
    }
}


