#pragma warning disable CS8618
namespace Modrinth.RestClient.Models;

public class DonationUrl
{
    /// <summary>
    /// The ID of the donation platform
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// The donation platform this link is to
    /// </summary>
    public string Platform { get; set; }
    
    /// <summary>
    /// The URL of the donation platform and user
    /// </summary>
    public string Url { get; set; }
}