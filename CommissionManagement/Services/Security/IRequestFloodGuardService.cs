namespace CommissionManagement.Services.Security
{
	public interface IRequestFloodGuardService
	{
		bool IsDuplicate(string fingerprint, TimeSpan ttl);
	}
}