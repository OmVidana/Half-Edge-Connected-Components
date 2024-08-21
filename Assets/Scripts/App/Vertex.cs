using UnityEngine;

/// <summary>
/// Represents a vertex used for Mesh Rendering and Processing.
/// </summary>
public class Vertex
{
    /// <summary>
    /// Gets the origin point of the vertex.
    /// </summary>
    public Vector3 Origin { get; private set; }

    /// <summary>
    /// Gets or sets the half-edge associated with this vertex.
    /// </summary>
    public HalfEdge HalfEdge { get; set; }

    /// <summary>
    /// Gets or sets the normal vector at this vertex.
    /// </summary>
    public Vector3 Normal { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vertex"/> class.
    /// </summary>
    /// <param name="origin">The origin point of the vertex.</param>
    public Vertex(Vector3 origin)
    {
        Origin = origin;
        HalfEdge = null;
        Normal = Vector3.zero;
    }
}
