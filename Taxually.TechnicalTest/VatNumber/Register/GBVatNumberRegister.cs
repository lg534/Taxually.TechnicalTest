using Taxually.TechnicalTest.DTOModel;

namespace Taxually.TechnicalTest.VatNumber.Register
{
    public class GBVatNumberRegister : IVatNumberRegister
    {
        public bool CanProcess(VatRegistrationRequest vatNumberRequest) => vatNumberRequest.Country.Equals("GB", StringComparison.InvariantCultureIgnoreCase);

        public async Task RegisterVatNumberAsync(VatRegistrationRequest vatNumberRequest)
        {
            // UK has an API to register for a VAT number
            var httpClient = new TaxuallyHttpClient();
            await httpClient.PostAsync("https://api.uktax.gov.uk", vatNumberRequest);
        }
    }
}
