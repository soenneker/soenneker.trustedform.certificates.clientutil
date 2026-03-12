using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.TrustedForm.Certificates.ClientUtil.Abstract;
using Soenneker.TrustedForm.Client.Registrars;

namespace Soenneker.TrustedForm.Certificates.ClientUtil.Registrars;

/// <summary>
/// A .NET thread-safe singleton HttpClient for GitHub
/// </summary>
public static class TrustedFormCertificatesClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="TrustedFormCertificatesClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddTrustedFormCertificatesClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddTrustedFormClientAsSingleton().TryAddSingleton<ITrustedFormCertificatesClientUtil, TrustedFormCertificatesClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="TrustedFormCertificatesClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddTrustedFormCertificatesClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddTrustedFormClientAsSingleton().TryAddScoped<ITrustedFormCertificatesClientUtil, TrustedFormCertificatesClientUtil>();

        return services;
    }
}