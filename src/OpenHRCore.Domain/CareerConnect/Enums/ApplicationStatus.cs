namespace OpenHRCore.Domain.CareerConnect.Enums
{

    public enum ApplicationStatus
    {
        // Applied Stage
        Applied_New, // Candidate has applied
        Applied_InProgress, // Application is under review

        // Screening Stage
        Screening_Shortlisted, // Candidate passed screening
        Screening_Rejected, // Candidate rejected after screening
        Screening_InProgress, // Screening in progress

        // Interview Stage
        Interview_Scheduled, // Interview scheduled
        Interview_Completed, // Interview completed
        Interview_NoShow, // Candidate no-show for interview
        Interview_Rescheduled, // Interview rescheduled

        // Offer Stage
        Offer_OfferExtended, // Offer extended to candidate
        Offer_OfferAccepted, // Candidate accepted the offer
        Offer_OfferRejected, // Candidate rejected the offer
        Offer_OfferWithdrawn // Offer withdrawn by company
    }

}
