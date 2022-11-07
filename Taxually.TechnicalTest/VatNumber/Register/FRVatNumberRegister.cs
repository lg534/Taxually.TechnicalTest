using System.Text;
using Taxually.TechnicalTest.DTOModel;

namespace Taxually.TechnicalTest.VatNumber.Register
{
    public class FRVatNumberRegister : IVatNumberRegister
    {
        public bool CanProcess(VatRegistrationRequest vatNumberRequest) => vatNumberRequest.Country.Equals("FR", StringComparison.InvariantCultureIgnoreCase);

        public async Task RegisterVatNumberAsync(VatRegistrationRequest vatNumberRequest)
        {
            // France requires an excel spreadsheet to be uploaded to register for a VAT number
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{vatNumberRequest.CompanyName}{vatNumberRequest.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            var excelQueueClient = new TaxuallyQueueClient();
            // Queue file to be processed
            await excelQueueClient.EnqueueAsync("vat-registration-csv", csv);
        }
    }
}
