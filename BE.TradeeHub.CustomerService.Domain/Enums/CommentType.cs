using System.ComponentModel;

namespace BE.TradeeHub.CustomerService.Domain.Enums;

public enum CommentType
{
    [Description("General")]
    General,
    [Description("Appointment")]
    Appointment,
    [Description("Quote")]
    Quote,
    [Description("Job")]
    Job,
    [Description("Invoice")]
    Invoice,
}