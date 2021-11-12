using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.VinCheck
{
    public class GetVinInfoQueryValidation : AbstractValidator<GetVinInfoQuery>
    {
        public GetVinInfoQueryValidation()
        {
            RuleFor(x => x.VinNumber).Must(vin => ValidateNumber(vin)).WithMessage("Incomplete VIN. Invalid Characters Present. ");
        }

        private bool ValidateNumber(object vinNumber)
        {
            string vin = vinNumber as string;
            if (vin?.Length != 17)
            {
                return false;
            }

            return GetCheckDigit(vin) == vin[8];
        }

        private static int Transliterate(char c)
        {
            return "0123456789.ABCDEFGH..JKLMN.P.R..STUVWXYZ".IndexOf(c) % 10;
        }

        private static char GetCheckDigit(string vin)
        {
            string map = "0123456789X";
            string weights = "8765432X098765432";
            int sum = 0;
            for (int i = 0; i < 17; ++i)
            {
                sum += Transliterate(vin[i]) * map.IndexOf(weights[i]);
            }
            return map[sum % 11];
        }
    }
}