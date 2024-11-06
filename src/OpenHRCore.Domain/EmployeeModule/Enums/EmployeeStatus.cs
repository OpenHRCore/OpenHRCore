namespace OpenHRCore.Domain.EmployeeModule.Enums
{
    /// <summary>
    /// Represents the current status of an employee within the organization.
    /// </summary>
    public enum EmployeeStatus
    {
        /// <summary>
        /// EmployeeInfo is currently active and working.
        /// </summary>
        Active = 0,

        /// <summary>
        /// EmployeeInfo is on leave (e.g., annual, sick, parental).
        /// </summary>
        OnLeave = 1,

        /// <summary>
        /// EmployeeInfo's activities are temporarily suspended.
        /// </summary>
        Suspended = 2,

        /// <summary>
        /// EmployeeInfo has been terminated or has resigned.
        /// </summary>
        Terminated = 3,

        /// <summary>
        /// EmployeeInfo has retired from active service.
        /// </summary>
        Retired = 4,

        /// <summary>
        /// EmployeeInfo is currently under probation.
        /// </summary>
        Probation = 5,

        /// <summary>
        /// EmployeeInfo is not currently active, but not formally terminated or resigned.
        /// </summary>
        Inactive = 6
    }
}
