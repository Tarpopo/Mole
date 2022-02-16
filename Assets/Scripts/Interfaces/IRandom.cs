using System;
using UnityEngine;

namespace Interfaces
{
    public interface IRandom
    {
        float ReturnChance { get;}
        GameObject Enemy { get; }
    }
}