using Taxually.TechnicalTest.DTOModel;

namespace Taxually.TechnicalTest.VatNumber.Register
{
    public interface IVatNumberRegister
    {
        public bool CanProcess(VatRegistrationRequest vatNumberRequest);
        public Task RegisterVatNumberAsync(VatRegistrationRequest vatNumberRequest);
    }
}
