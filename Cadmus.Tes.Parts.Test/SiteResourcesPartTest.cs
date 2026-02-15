using Cadmus.Core;
using Cadmus.Seed.Tes.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Tes.Parts.Test;

public class SiteResourcesPartTest
{
    private static SiteResourcesPart GetPart()
    {
        SiteResourcesPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (SiteResourcesPart)seeder.GetPart(item, null, null)!;
    }

    private static SiteResourcesPart GetEmptyPart()
    {
        return new SiteResourcesPart
        {
            ItemId = Guid.NewGuid().ToString(),
            RoleId = "some-role",
            CreatorId = "zeus",
            UserId = "another",
        };
    }

    [Fact]
    public void Part_Is_Serializable()
    {
        SiteResourcesPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        SiteResourcesPart part2 =
            TestHelper.DeserializePart<SiteResourcesPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Resources.Count, part2.Resources.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        SiteResourcesPart part = GetPart();
        part.Resources.Clear();

        List<DataPin> pins = [.. part.GetDataPins(null)];

        Assert.Single(pins);
        DataPin pin = pins[0];
        Assert.Equal("tot-count", pin.Name);
        TestHelper.AssertPinIds(part, pin);
        Assert.Equal("0", pin.Value);
    }

    [Fact]
    public void GetDataPins_Entries_Ok()
    {
        SiteResourcesPart part = GetEmptyPart();

        for (int n = 1; n <= 3; n++)
        {
            // TODO add entry to part setting its pin-related
            // properties in a predictable way, so we can test them
        }

        List<DataPin> pins = [.. part.GetDataPins(null)];

        Assert.Equal(5, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        // TODO: assert counts and values e.g.:
        // pin = pins.Find(p => p.Name == "pos-bottom-count");
        // Assert.NotNull(pin);
        // TestHelper.AssertPinIds(part, pin!);
        // Assert.Equal("2", pin.Value);
    }
}
