[![](https://img.shields.io/nuget/v/soenneker.trustedform.certificates.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.trustedform.certificates.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.trustedform.certificates.clientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.trustedform.certificates.clientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.trustedform.certificates.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.trustedform.certificates.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.trustedform.certificates.clientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.trustedform.certificates.clientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.TrustedForm.Certificates.ClientUtil
Provides lazily initialized, authenticated access to the TrustedForm Certificate API v4 client.

## Installation

```bash
dotnet add package Soenneker.TrustedForm.Certificates.ClientUtil
```

## Configuration

```json
{
  "ActiveProspect": {
    "TrustedForm": {
      "ApiKey": "your-api-key"
    }
  }
}
```

Supply the raw TrustedForm API key. The utility creates the required HTTP Basic credential with username `API` and adds `Api-Version: 4.0` to requests.

## Registration

```csharp
using Soenneker.TrustedForm.Certificates.ClientUtil.Registrars;

services.AddTrustedFormCertificatesClientUtilAsScoped();
```

The scoped utility owns its generated-client wrapper. Its underlying TrustedForm HTTP provider remains singleton and is reused after the utility is disposed. Singleton utility registration is also available through `AddTrustedFormCertificatesClientUtilAsSingleton()`.

## Retain and match a certificate

```csharp
using Soenneker.TrustedForm.Certificates.OpenApiClient;
using Soenneker.TrustedForm.Certificates.OpenApiClient.Item;
using Soenneker.TrustedForm.Certificates.OpenApiClient.Models;

Uri certificateUri = new(certificateUrl);

if (!certificateUri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) ||
    !certificateUri.Host.Equals("cert.trustedform.com", StringComparison.OrdinalIgnoreCase))
{
    throw new InvalidOperationException("The certificate URL is not a TrustedForm certificate URL.");
}

string certificateId = certificateUri.AbsolutePath.Trim('/');
TrustedFormCertificatesOpenApiClient client =
    await certificateClients.Get(cancellationToken);

var body = new WithCert_PostRequestBody
{
    MatchLead = new MatchLeadParameters
    {
        MatchLeadEmailParameters = new MatchLeadEmailParameters
        {
            Email = leadEmail
        }
    },
    Retain = new RetainParameters
    {
        Reference = leadId,
        Vendor = leadVendor
    }
};

WithCert_PostResponse? result =
    await client[certificateId].PostAsync(body, cancellationToken: cancellationToken);
```

Validate the certificate host before extracting the ID. Sending an authenticated request to a caller-supplied host can disclose the TrustedForm API credential.

Certificate v4 operations are contracted and billed separately. Request only the `retain`, `match_lead`, `insights`, or `verify` operations your workflow intends to purchase, and inspect each operation's result rather than treating an HTTP success alone as business success.

Do not log API keys, certificate URLs, lead matching values, insights, or response bodies containing identity data.
