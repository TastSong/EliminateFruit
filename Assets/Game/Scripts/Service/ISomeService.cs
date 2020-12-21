using UnityEngine;
using System;

public interface ISomeService
{
    string Name { get; set; }
    void AddListener(Action<string> listener);
}
