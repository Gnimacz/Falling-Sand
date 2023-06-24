using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element
{
    Vector2Int position;
    public Vector2Int Position
    {
        get { return position; }
        set { position = value; }
    }
}

class ElementAttributes
{
    int health;
    int density;
    int flammability;
    int heat;
    int conductivity;
    int color;

    public int GetHealth()
    {
        return health;
    }

    public int GetDensity()
    {
        return density;
    }

    public int GetFlammability()
    {
        return flammability;
    }

    public int GetHeat()
    {
        return heat;
    }

    public int GetConductivity()
    {
        return conductivity;
    }

    public int GetColor()
    {
        return color;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void SetDensity(int density)
    {
        this.density = density;
    }

    public void SetFlammability(int flammability)
    {
        this.flammability = flammability;
    }

    public void SetHeat(int heat)
    {
        this.heat = heat;
    }

    public void SetConductivity(int conductivity)
    {
        this.conductivity = conductivity;
    }

    public void SetColor(int color)
    {
        this.color = color;
    }

    public ElementAttributes(int health, int density, int flammability, int heat, int conductivity, int color)
    {
        this.health = health;
        this.density = density;
        this.flammability = flammability;
        this.heat = heat;
        this.conductivity = conductivity;
        this.color = color;
    }
}

public enum ElementType
{
    Sand,
    Water,
    Stone
}