using NotkaMobile.Service.Reference;

namespace NotkaMobile.Services.Abstract
{
	public abstract class ADataStore
	{
		protected readonly NotkaMobileService _service;
		protected ADataStore()
		{
			//Use this code to test locally - localhost does not have SSL certificate
			var handler = new HttpClientHandler();
#if DEBUG
			//manual certificate setup
			handler.ClientCertificateOptions = ClientCertificateOption.Manual;
			handler.ServerCertificateCustomValidationCallback =
				(httpRequestMessage, cert, cetChain, policyErrors) =>
				{
					return true;
				};
#endif
			var client = new HttpClient(handler);
			client.Timeout = TimeSpan.FromSeconds(10);
			
			//local Swagger RestAPI address (suitable for Windows Machine)
			_service = new NotkaMobileService("https://localhost:7089/", client);

			//Swagger RestAPI address for Android (use only one!)
			//_service = new NotkaMobileService("https://10.0.2.2:7089/", client);
		}
	}
}
