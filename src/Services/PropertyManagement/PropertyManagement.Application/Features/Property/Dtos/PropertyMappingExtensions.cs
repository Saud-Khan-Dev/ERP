public static class PropertyMappingExtensions
{
  public static PropertyDto ToDto(this Property property, GeoLocation? geoLocation) =>
      new(
        Id: property.Id.Value,
        PropertyCode: property.PropertyCode.Value,
        PropertyName: property.PropertyName.Value,
        PropertyType: property.PropertyType,
        OwnershipType: property.OwnershipType,
        AreaSqFt: property.AreaSqFt,
        CoveredAreaSqFt: property.CoveredAreaSqFt,
        LandAreaSqFt: property.LandAreaSqFt,
        Status: property.Status,
        GeoLocation: geoLocation?.ToDto()
      );

  public static GeoLocationDto ToDto(this GeoLocation geoLocation) =>
      new(
        Id: geoLocation.Id.Value,
        Code: geoLocation.Code.Value,
        Latitude: geoLocation.Latitude,
        Longitude: geoLocation.Longitude,
        Elevation: geoLocation.Elevation,
        GeoFenceRadius: geoLocation.GeoFenceRadius,
        CoordinateSystem: geoLocation.CoordinateSystem
      );

  public static AttributeDefinitionDto ToDto(this AttributeDefinition definition) =>
      new(
        Id: definition.Id.Value,
        Group: definition.Group,
        Code: definition.Code.Value,
        Label: definition.Label.Value,
        DataType: definition.DataType,
        IsRequired: definition.IsRequired,
        Options: definition.Options,
        DefaultValue: definition.DefaultValue,
        DisplayOrder: definition.DisplayOrder,
        IsActive: definition.IsActive
      );

  public static PropertyDocumentDto ToDto(this PropertyDocument document) =>
      new(
        Id: document.Id.Value,
        PropertyId: document.PropertyId.Value,
        DocumentType: document.DocumentType,
        RelatedGroup: document.RelatedGroup,
        FileUrl: document.FileUrl.Value,
        FileName: document.FileName,
        FileSizeBytes: document.FileSizeBytes,
        UploadedOn: document.UploadedOn,
        UploadedBy: document.UploadedBy,
        Remarks: document.Remarks
      );
}
