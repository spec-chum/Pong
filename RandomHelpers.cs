using Microsoft.Xna.Framework;
using System;

namespace Pong;

public static class RandomHelpers
{
    public static float NextSingle(float min, float max)
    {
        return (Random.Shared.NextSingle() * (max - min)) + min;
    }

    public static Vector2 NextVector2(float min, float max)
    {
        return new(NextSingle(min, max), NextSingle(min, max));
    }
}