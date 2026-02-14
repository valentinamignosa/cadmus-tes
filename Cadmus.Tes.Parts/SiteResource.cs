using Cadmus.Geo.Parts;
using Cadmus.Refs.Bricks;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Tes.Parts;

/// <summary>
/// A resource in a site, e.g. a quarry, a mine, a water source, etc.
/// </summary>
public class SiteResource
{
    /// <summary>
    /// The type of the resource, e.g. "quarry", "mine", "water source", etc.
    /// Usually from thesaurus <c>site-resource-types</c>.
    /// </summary>
    public string Type { get; set; } = "";

    /// <summary>
    /// Gets or sets an optional tag or label associated with the object.
    /// Usually from thesaurus <c>site-resource-tags</c>.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// Gets or sets the list of features for this resource.
    /// Usually from thesaurus <c>site-resource-features</c>.
    /// </summary>
    public List<string>? Features { get; set; }

    /// <summary>
    /// Gets or sets the asserted location of the resource within its site.
    /// </summary>
    public AssertedLocation? Location { get; set; }

    /// <summary>
    /// Gets or sets the asserted historical date associated with the resource.
    /// </summary>
    public AssertedHistoricalDate? Date { get; set; }

    /// <summary>
    /// Gets or sets the collection of decorated counts associated with the
    /// resource, e.g. the estimated income in talents, the number of marble
    /// blocks found in a quarry, etc.
    /// </summary>
    public List<DecoratedCount>? Counts { get; set; }

    /// <summary>
    /// Gets or sets an optional free text note for this resource.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Returns a string that represents this resource.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(Type);
        if (!string.IsNullOrEmpty(Tag)) sb.Append($" ({Tag})");
        if (Features != null && Features.Count > 0)
            sb.Append($": {string.Join(", ", Features)}");
        return sb.ToString();
    }
}
