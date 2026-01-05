using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.TrustedForm.Certificates.ClientUtil.Abstract;
using Soenneker.TrustedForm.Certificates.OpenApiClient;
using Soenneker.TrustedForm.Client.Abstract;
using Soenneker.Utils.AsyncSingleton;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Extensions.ValueTask;
using Soenneker.Kiota.GenericAuthenticationProvider;

namespace Soenneker.TrustedForm.Certificates.ClientUtil;

///<inheritdoc cref="ITrustedFormCertificatesClientUtil"/>
public sealed class TrustedFormCertificatesClientUtil : ITrustedFormCertificatesClientUtil
{
    private readonly AsyncSingleton<TrustedFormCertificatesOpenApiClient> _client;
    private readonly ITrustedFormClient _httpClientUtil;
    private readonly IConfiguration _configuration;

    public TrustedFormCertificatesClientUtil(ITrustedFormClient httpClientUtil, IConfiguration configuration)
    {
        _httpClientUtil = httpClientUtil;
        _configuration = configuration;
        _client = new AsyncSingleton<TrustedFormCertificatesOpenApiClient>(CreateClient);
    }

    private async ValueTask<TrustedFormCertificatesOpenApiClient> CreateClient(CancellationToken token)
    {
        HttpClient httpClient = await _httpClientUtil.Get(token).NoSync();

        var apiKey = _configuration.GetValueStrict<string>("ActiveProspect:TrustedForm:ApiKey");

        var provider = new GenericAuthenticationProvider(headerValue: $"Basic {apiKey}", additionalHeaders: new Dictionary<string, string> { { "Api-Version", "4.0" } });

        var requestAdapter = new HttpClientRequestAdapter(provider, httpClient: httpClient);

        return new TrustedFormCertificatesOpenApiClient(requestAdapter);
    }

    public ValueTask<TrustedFormCertificatesOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
