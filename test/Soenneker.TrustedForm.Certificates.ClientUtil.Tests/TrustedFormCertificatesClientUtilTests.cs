using Soenneker.TrustedForm.Certificates.ClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.TrustedForm.Certificates.ClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class TrustedFormCertificatesClientUtilTests : HostedUnitTest
{
    private readonly ITrustedFormCertificatesClientUtil _kiotaclient;

    public TrustedFormCertificatesClientUtilTests(Host host) : base(host)
    {
        _kiotaclient = Resolve<ITrustedFormCertificatesClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
