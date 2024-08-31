﻿namespace OpenHRCore.SharedKernel.Domain.Entities
{
    /// <summary>
    /// Represents the base entity for all OpenHR Core entities.
    /// </summary>
    public abstract class OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        public required string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last updated the entity.
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the entity is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}