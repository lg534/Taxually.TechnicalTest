using System.Xml.Serialization;
using Taxually.TechnicalTest.DTOModel;

namespace Taxually.TechnicalTest.VatNumber.Register
{
    public class DEVatNumberRegister : IVatNumberRegister
    {
        public bool CanProcess(VatRegistrationRequest vatNumberRequest) => vatNumberRequest.Country.Equals("DE", StringComparison.InvariantCultureIgnoreCase);

        public async Task RegisterVatNumberAsync(VatRegistrationRequest vatNumberRequest)
        {
            // Germany requires an XML document to be uploaded to register for a VAT number
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
                serializer.Serialize(stringwriter, vatNumberRequest);
                var xml = stringwriter.ToString();
                var xmlQueueClient = new TaxuallyQueueClient();
                // Queue xml doc to be processed
                await xmlQueueClient.EnqueueAsync("vat-registration-xml", xml);
            }
        }
    }
}
