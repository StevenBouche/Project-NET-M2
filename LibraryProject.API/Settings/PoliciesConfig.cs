namespace LibraryProject.API.Settings
{
    public class PolicyEntry
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Allowed { get; set; } = new();
    }

    public class PoliciesConfig
    {
        public List<PolicyEntry> AllowPolicies { get; set; } = new();
    }
}