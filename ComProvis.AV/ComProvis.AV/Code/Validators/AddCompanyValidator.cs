
using ComProvis.AV.Services;
using FluentValidation;
using System.Threading.Tasks;

namespace ComProvis.AV.Code.Validators
{
    public class AddCompanyValidator : AbstractValidator<Company>
    {
        private IDataService _dataService { get; set; }
        public AddCompanyValidator(IDataService dataService)
        {
            _dataService = dataService;

            RuleFor(x => x.ExternalId).
                NotEmpty().WithMessage(Resource.ErrObjectIsNull)
                .MustAsync((x, cancellation) => {
                    return CustomerExistAsync(x);
                }).WithMessage(Resource.ErrCustomerExist);
            RuleFor(x => x.Name).
                Length(1, 255).WithMessage(Resource.ErrObjectLength255).
                NotEmpty().WithMessage(Resource.ErrObjectIsNull);
            RuleFor(x => x.ContactEmail).
               Length(1, 255).WithMessage(Resource.ErrObjectLength255).
               Matches(Resource.RegexEmail).WithMessage(Resource.ErrObjectIsNotValid);
        }

        async Task<bool> CustomerExistAsync(string externalCustomerId)
        {
            var product =  await _dataService.CompanyService.ExternalGetAsync(externalCustomerId);
            if (product?.Id != null) return false;
            return true;
        }
    }
}
