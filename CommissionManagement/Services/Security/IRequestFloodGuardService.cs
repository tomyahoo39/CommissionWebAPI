namespace CommissionManagement.Services.Security
{
	public interface IRequestFloodGuardService
	{
		bool IsDuplicate(string fingerprint, TimeSpan ttl);
		bool TryGet<T>(string key, out T? value);
		void Set<T>(string key, T value, TimeSpan ttl);
		void Remove(string key);
	}
}