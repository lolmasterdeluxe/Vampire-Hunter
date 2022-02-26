using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Muhammad Rifdi bin Sabbri 
 * Created: 27/2/2022
 */
public static class RenderExtension
{
    /// <summary>
    /// Determines whether the renderer is visible from the specified camera.
    /// </summary>
    /// <param name="renderer">The renderer to check for visibility.</param>
    /// <param name="camera">The camera to check against.</param>
    /// <returns>true if the renderer is visible to the camera; otherwise false.</returns>
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
