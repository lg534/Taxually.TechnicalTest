using Taxually.TechnicalTest.DTOModel;
using Taxually.TechnicalTest.VatNumber.Register;

namespace Taxually.TechnicalTest.VatNumber.Selector
{
    // This class can be created as part of some dependency injection framework, as a singleton
    public class VatNumberRegisterSelector
    {
        private List<IVatNumberRegister> VatNumberRegisterList = new List<IVatNumberRegister>();

        public VatNumberRegisterSelector()
        {
            // these can be added from a central registration class
            AddRegister<DEVatNumberRegister>();
            AddRegister<FRVatNumberRegister>();
            AddRegister<GBVatNumberRegister>();
        }

        public void AddRegister<T>() where T : class, IVatNumberRegister, new() => VatNumberRegisterList.Add(new T());

        public async Task<bool> RegisterVatNumberAsync(VatRegistrationRequest vatNumberRequest)
        {
            bool isProcessed = false;

            foreach (var register in VatNumberRegisterList)
            {
                if (register.CanProcess(vatNumberRequest))
                {
                    await register.RegisterVatNumberAsync(vatNumberRequest);
                    isProcessed = true;
                }
            }

            return isProcessed;
        }
    }
}
