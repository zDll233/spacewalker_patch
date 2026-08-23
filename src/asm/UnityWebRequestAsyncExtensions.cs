using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.Networking;

public static class UnityWebRequestAsyncExtensions
{
	public static TaskAwaiter<UnityWebRequest> GetAwaiter(this UnityWebRequestAsyncOperation operation)
	{
		TaskCompletionSource<UnityWebRequest> tcs = new TaskCompletionSource<UnityWebRequest>();
		operation.completed += delegate
		{
			tcs.SetResult(operation.webRequest);
		};
		return tcs.Task.GetAwaiter();
	}
}
