using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApp.Server.Data.Models;

public record PaymentDetail
{
    [Key]
    public int PaymentDetailId { get; set; }
    public string CardOwnerName { get; set; } = string.Empty; // TODO: Specify column type The method used in the tutorial is suspicious: https://youtu.be/OZGdKYzUYvU?si=Q95b_NtbBGUTJo5L&t=294
    public string CardNumber { get; set; } = string.Empty;
    public string ExpirationDate { get; set; } = string.Empty;
    public string SecurityCode { get; set; } = string.Empty;
}
