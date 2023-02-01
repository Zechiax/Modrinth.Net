using System.Text;

namespace Modrinth.Client;

/// <summary>
/// Helper class for creating a user agent string for the Modrinth API client
/// </summary>
public class UserAgent
{
    /// <summary>
    /// Name of the project
    /// </summary>
    public string? ProjectName { get; set; }
    /// <summary>
    /// Version of the project
    /// </summary>
    public string? ProjectVersion { get; set; }
    
    /// <summary>
    /// Email or website of the project
    /// </summary>
    public string? Contact { get; set; }
    /// <summary>
    /// GitHub username of the project
    /// </summary>
    public string? GitHubUsername { get; set; }

    /// <summary>
    /// Creates a user agent string based on provided information
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        // Needs checks for null
        var sb = new StringBuilder();
        
        if (GitHubUsername != null)
        {
            sb.Append(GitHubUsername);
            sb.Append('/');
        }
        
        if (ProjectName != null)
        {
            sb.Append(ProjectName);
            sb.Append('/');
        }
        
        if (ProjectVersion != null)
        {
            sb.Append(ProjectVersion);
        }
        
        if (Contact != null)
        {
            sb.Append(" (");
            sb.Append(Contact);
            sb.Append(')');
        }
        
        return sb.ToString();
    }
}