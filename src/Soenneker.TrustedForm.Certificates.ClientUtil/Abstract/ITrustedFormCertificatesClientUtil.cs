using Soenneker.TrustedForm.Certificates.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.TrustedForm.Certificates.ClientUtil.Abstract;

/// <summary>
/// Provides cached access to an authenticated TrustedForm Certificate API v4 client.
/// </summary>
public interface ITrustedFormCertificatesClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached TrustedForm Certificate API client.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<TrustedFormCertificatesOpenApiClient> Get(CancellationToken cancellationToken = default);
}
