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
			//ręczne ustawienie certyfikatu
			handler.ClientCertificateOptions = ClientCertificateOption.Manual;
			handler.ServerCertificateCustomValidationCallback =
				(httpRequestMessage, cert, cetChain, policyErrors) =>
				{
					return true;
				};
#endif
			var client = new HttpClient(handler);
			//local Swagger RestAPI address
			_service = new NotkaMobileService("https://localhost:7089/", client);
		}
	}
}
