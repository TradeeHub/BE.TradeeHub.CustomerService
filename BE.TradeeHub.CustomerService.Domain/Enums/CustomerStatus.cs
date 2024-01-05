using System.ComponentModel;

namespace BE.TradeeHub.CustomerService.Domain.Enums;

public enum CustomerStatus
{
    [Description("Lead")]
    Lead,                // Initial potential customer
    [Description("Appointment Set")]
    AppointmentSet,      // An appointment has been made
    [Description("Quote Provided")]
    QuoteProvided,       // A quote has been provided to the customer
    [Description("Quote Accepted")]
    QuoteAccepted,       // The customer has accepted the quote
    [Description("Work Scheduled")]
    WorkScheduled,       // Work has been scheduled
    [Description("Work In Progress")]
    WorkInProgress,      // Work is currently being performed
    [Description("Work Completed")]
    WorkCompleted,       // Work has been completed
    [Description("Invoice Sent")]
    InvoiceSent,         // Invoice has been sent to the customer
    [Description("Payment Received")]
    PaymentReceived,     // Payment has been received from the customer
    [Description("Follow Up Required")]
    FollowUpRequired,    // Follow-up action is required
    [Description("Closed Lost")]
    ClosedLost,          // The customer decided not to proceed
    [Description("Closed")]
    Completed            // The job was successful and customer is satisfied
}