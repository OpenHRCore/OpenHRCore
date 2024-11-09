﻿namespace OpenHRCore.Application.Workforce.DTOs.OU
{
    public class GetOrganizationUnitResponse
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? ParentOrganizationUnitId { get; set; }
        public string? ParentOrganizationCode { get; set; }
        public string? ParentOrganizationName { get; set; }
        public string? ParentOrganizationDescription { get; set; }
        public required int SortOrder { get; set; }
    }
}