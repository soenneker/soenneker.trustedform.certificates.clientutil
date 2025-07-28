using Soenneker.TrustedForm.Certificates.ClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.TrustedForm.Certificates.ClientUtil.Tests;

[Collection("Collection")]
public sealed class TrustedFormCertificatesClientUtilTests : FixturedUnitTest
{
    private readonly ITrustedFormCertificatesClientUtil _kiotaclient;

    public TrustedFormCertificatesClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _kiotaclient = Resolve<ITrustedFormCertificatesClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
