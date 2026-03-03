# Cadmus TES

Backend for the Cadmus TES project.

🐋 Quick Docker image build:

```sh
docker buildx create --use

docker buildx build . --platform linux/amd64,linux/arm64,windows/amd64 -t vedph2020/cadmus-tes-api:0.0.3 -t vedph2020/cadmus-tes-api:latest --push
```

(replace with the current version).

## TES Parts

### SiteResourcesPart

This part lists the resources present in a site. Each resource can contain tag, type, any number of features, location, date, counts, and a free note.

- `resources` (`SiteResource[]`):
  - `eid` (`string`): the resource's entity identifier.
  - `type`\* (`string`, 📚 `site-resource-types`): the type of the resource, e.g. "quarry", "mine", "water source", etc.
  - `tag` (`string`, 📚 `site-resource-tags`): an optional tag or label associated with the object, e.g. "cava A", "pozzo B".
  - `features` (`string[]`, 📚 `site-resource-features` hierarchical): the list of features for this resource, including evidences in their own branch.
  - `location` (`AssertedLocation`: see [brick](https://github.com/vedph/cadmus-bricks-shell-v3/blob/master/projects/myrmidon/cadmus-geo-location/README.md) and its [demo](https://cadmus-bricks-v3.fusi-soft.com/geo/geo-location-editor)):
    - `eid` (`string`)
    - `label`\* (`string`)
    - `latitude`\* (`double`)
    - `longitude`\* (`double`)
    - `altitude` (`double`) in mt.
    - `radius` (`double`): uncertainty radius in mt.
    - `geometry` (`string`) in WKT.
  - `date` (`AssertedHistoricalDate`):
    - `tag` (`string` 📚 `asserted-historical-date-tags`)
    - `a`\* (🧱 [Datation](https://github.com/vedph/cadmus-bricks/blob/master/docs/datation.md)):
      - `value`\* (`int`): the numeric value of the point. Its interpretation depends on other points properties: it may represent a year or a century, or a span between two consecutive Gregorian years.
      - `isCentury` (`boolean`): true if value is a century number; false if it's a Gregorian year.
      - `isSpan` (`boolean`): true if the value is the first year of a pair of two consecutive years. This is used for calendars which span across two Gregorian years, e.g. 776/5 BC.
      - `slide` (`int`): a "slide" delta to be added to value. For instance, value=1230 and slide=10 means 1230-1240; this is not a range in the sense of `HistoricalDate` with its A and B points; it's just a relatively undeterminated point, allowed to move between 1230 and 1240. This means that we can still have a range, like A=1230-1240 and B=1290. A slide is represented by the end year/century value prefixed by `:` in its parsable string. So, we can have strings like `1230:1240--1290` for range A=1230-1240 and B=1290, or even `1230:1240--1290:1295`; all combinations are possible. With negative (BC) values we have e.g. `810:805 BC` implying slide=5.
      - `month` (`short`): the month number (1-12) or 0.
      - `day` (`short`): the day number (1-31) or 0.
      - `isApproximate` (`boolean`): true if the point is approximate ("about").
      - `isDubious` (`boolean`): true if the point is dubious ("perhaphs").
      - `hint` (`string`): a short textual hint used to better explain or motivate the datation point.
    - `b` (🧱 [Datation](https://github.com/vedph/cadmus-bricks/blob/master/docs/datation.md))
    - `assertion` (`Assertion`):
      - `tag` (`string`)
      - `rank` (`short`)
      - `references` (🧱 [DocReference[]](https://github.com/vedph/cadmus-bricks/blob/master/docs/doc-reference.md)):
        - `type` (`string` 📚 `doc-reference-types`)
        - `tag` (`string` 📚 `doc-reference-tags`)
        - `citation` (`string`)
        - `note` (`string`)
  - `counts` ([DecoratedCount[]](https://github.com/vedph/cadmus-general/blob/master/docs/decorated-counts.md)): collection of decorated counts associated with the resource, e.g. the estimated income in talents, the number of marble blocks found in a quarry, etc.
    - `id`\* (`string` 📚 `site-resource-count-ids`)
    - `tag` (`string` 📚 `site-resource-count-tags`)
    - `value`\* (`int`)
    - `note` (`string`)
  - `note` (`string`)

## Parts Matrix

| part         | inscription    | artifact                 | site                 | cult                           | iconography |
| ------------ | -------------- | ------------------------ | -------------------- | ------------------------------ | ----------- |
| categories   | ins-fn ins-lng | art-type art-mat art-ctx | site-type site-feats | cult-type cult-gods cult-feats | ico-sub     |
| comment      | X              | X                        | X                    | X                              | X           |
| dates        | X              | X                        | X                    |                                |             |
| fragments    | X              |                          |                      |                                |             |
| links        | X              | X                        | X                    | X                              | X           |
| locations    | X              | X                        | X                    |                                |             |
| metadata     | X              | X                        | X                    | X                              | X           |
| support      | X              |                          |                      |                                |             |
| techniques   | X              |                          |                      |                                |             |
| toponyms     |                |                          | X                    |                                |             |
| note         | X transl       | X                        | X                    | X                              | X           |
| references   | X              | X                        | X                    | X                              | X           |
| resources    |                |                          | site-res site-prod   |                                |             |
| states       | X              |                          |                      |                                |             |
| scripts      | X              |                          |                      |                                |             |
| signs        | X              |                          |                      |                                |             |
| text         | X              |                          |                      |                                |             |
| apparatus=   | X              |                          |                      |                                |             |
| comment=     | X              |                          |                      |                                |             |
| chronology=  | X              |                          |                      |                                |             |
| orthography= | X              |                          |                      |                                |             |
| ligatures=   | X              |                          |                      |                                |             |
| links=       | X              |                          |                      |                                |             |


## External Links Policy

External links connect a TES item to an authoritative external resource.

## Usage

Use external links for:

- Authority databases (e.g. Pleiades, GeoNames)

- Epigraphic databases (e.g. EDR, PHI, Trismegistos)

- Archaeological repositories (e.g. Arachne, ADS)

- Iconographic databases (e.g. LIMC, Beazley Archive, CVA)

- Digital scholarly projects and online reference works

Each external link must include:

- `label` – human-readable description
- `gid` – full, stable URL of the external record
- `scope` – lowercase identifier of the provider (controlled list)

### GID Rule

The `gid` must contain the complete and stable URL of the external record.

Numeric identifiers alone should be avoided unless explicitly standardized.

### Scope Convention

Scopes must be lowercase and consistent across the project (e.g. `pleiades`, `edr`, `tm`, `zenon`, `limc`, etc.).

External lookup tools (when available) are optional UI helpers and do not affect the stored data model.


### History

### 0.0.3

- 2026-03-03: updated packages
- 2026-03-03: added External Links Policy section
