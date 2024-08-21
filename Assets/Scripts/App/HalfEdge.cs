/// <summary>
/// Represents a Half-Edge element in the half-Edge structure.
/// </summary>
public class HalfEdge
{
    /// <summary>
    /// Gets the origin vertex of the half-edge.
    /// </summary>
    public Vertex Origin { get; private set; }

    /// <summary>
    /// Gets or sets the twin half-edge.
    /// </summary>
    public HalfEdge Twin { get; set; }

    /// <summary>
    /// Gets or sets the next half-edge in the face.
    /// </summary>
    public HalfEdge Next { get; set; }

    /// <summary>
    /// Gets or sets the previous half-edge in the face.
    /// </summary>
    public HalfEdge Prev { get; set; }

    /// <summary>
    /// Gets or sets the face associated with this half-edge.
    /// </summary>
    public Face Face { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="HalfEdge"/> class.
    /// </summary>
    /// <param name="origin">The origin vertex of the half-edge.</param>
    public HalfEdge(Vertex origin)
    {
        Origin = origin;
        Twin = null;
        Next = null;
        Prev = null;
        Face = null;
    }
}
