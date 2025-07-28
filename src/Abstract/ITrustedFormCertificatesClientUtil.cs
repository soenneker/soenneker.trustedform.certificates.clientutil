using Soenneker.TrustedForm.Certificates.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.TrustedForm.Certificates.ClientUtil.Abstract;

/// <summary>
/// A .NET thread-safe singleton HttpClient for 
/// </summary>
public interface ITrustedFormCertificatesClientUtil : IDisposable, IAsyncDisposable
{
    ValueTask<TrustedFormCertificatesOpenApiClient> Get(CancellationToken cancellationToken = default);
}