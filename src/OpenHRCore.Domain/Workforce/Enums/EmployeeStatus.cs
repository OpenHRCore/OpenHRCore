namespace OpenHRCore.Domain.Workforce.Enums
{
    /// <summary>
    /// Represents the current status of an employee within the organization.
    /// </summary>
    public enum EmployeeStatus
    {
        /// <summary>
        /// Employee is currently active and working.
        /// </summary>
        Active = 0,

        /// <summary>
        /// Employee is on leave (e.g., annual, sick, parental).
        /// </summary>
        OnLeave = 1,

        /// <summary>
        /// Employee's activities are temporarily suspended.
        /// </summary>
        Suspended = 2,

        /// <summary>
        /// Employee has been terminated or has resigned.
        /// </summary>
        Terminated = 3,

        /// <summary>
        /// Employee has retired from active service.
        /// </summary>
        Retired = 4,

        /// <summary>
        /// Employee is currently under probation.
        /// </summary>
        Probation = 5,

        /// <summary>
        /// Employee is not currently active, but not formally terminated or resigned.
        /// </summary>
        Inactive = 6
    }
}
