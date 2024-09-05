namespace OpenHRCore.WorkForce.Domain.Enums
{
    public enum EmployeeStatus
    {
        Active,        // Employee is currently active and working
        OnLeave,       // Employee is on leave (could be annual, sick, etc.)
        Suspended,     // Employee's activities are temporarily suspended
        Terminated,    // Employee has been terminated or resigned
        Retired,       // Employee has retired from active service
        Probation,     // Employee is currently under probation
        Inactive       // Employee is not currently active, but not formally terminated or resigned
    }

}
