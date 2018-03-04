using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Heap class used in a*

public class PriorityQ<T> where T : IPQItem<T>
{
    T[] items;
    int itemCount;
    // constructor
    public PriorityQ(int size)
    {
        items = new T[size];
    }
    // add item
    public void Add( T item )
    {
        item.QIndex = itemCount;
        items[itemCount] = item;
        SortUp(item);
        itemCount++;
    }
    // set high prioriy
    private void SortUp( T item )
    {
        int parentIndex = (item.QIndex - 1) / 2;
        while( true )
        {
            T parent = items[parentIndex];
            if (item.CompareTo(parent) > 0 )
            {
                SwapItems( item, parent);
            }
            else
            {
                break;
            }
            parentIndex = (item.QIndex - 1) / 2;
        }
    }
    // set low priority
    private void SortDown( T item )
    {
        while( true)
        {
            int leftChildIndex = item.QIndex * 2 + 1;
            int rightChildIndex = item.QIndex * 2 + 2;
            int swapIndex = 0;
            if( leftChildIndex < itemCount )
            {
                swapIndex = leftChildIndex;
                if( rightChildIndex < itemCount )
                {
                    if( items[leftChildIndex].CompareTo(items[rightChildIndex]) < 0 )
                    {
                        swapIndex = rightChildIndex;
                    }
                }

                if( item.CompareTo(items[swapIndex]) < 0 )
                {
                    SwapItems(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
    // swap
    private void SwapItems( T first, T second )
    {
        items[first.QIndex] = second;
        items[second.QIndex] = first;
        int firstIndex = first.QIndex;
        first.QIndex = second.QIndex;
        second.QIndex = firstIndex;

    }
    // get min
    public T ExtractFirstItem()
    {
        T item = items[0];
        itemCount--;
        items[0] = items[itemCount];
        items[0].QIndex = 0;
        SortDown(items[0]);
        return item;
    }
    // check if an item in the heap
    public bool IsItemInQ( T item )
    {
        return Equals(items[item.QIndex], item);
    }
    // return size
    public int getSize()
    {
        return itemCount;
    }
    // update is called when the item priority gets lower
    public void Update( T item )
    {
        SortUp(item);
    }
}
// interface
public interface IPQItem<T> : IComparable<T>
{
    int QIndex
    {
        get;
        set;
    }
}
