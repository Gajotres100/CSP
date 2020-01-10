using ComProvis.AV.Services;
using FluentValidation;
using System.Threading.Tasks;

namespace ComProvis.AV.Code.Validators
{
    public class AddUserValidator : AbstractValidator<User>
    {
        private IDataService _dataService { get; set; }
        public AddUserValidator(IDataService dataService)
        {
            _dataService = dataService;

            RuleFor(x => x.Username).
                NotEmpty().WithMessage(Resource.ErrObjectIsNull);
            RuleFor(x => x.ExternalId).
                NotEmpty().WithMessage(Resource.ErrObjectIsNull).
                MustAsync((x, cancellation) => {
                    return UserExist(x);
                }).WithMessage(Resource.ErrUserExist);
            RuleFor(x => x.CompanyId).
                NotEmpty().WithMessage(Resource.ErrObjectIsNull).
                Must(CompanyExist).WithMessage(Resource.ErrCustomerDontExist);
            RuleFor(x => x.Email).
               Length(1, 255).WithMessage(Resource.ErrObjectLength255).
               Matches(Resource.RegexEmail).WithMessage(Resource.ErrObjectIsNotValid);
        }

        async Task<bool> UserExist(string externalId)
        {
            var user = await _dataService.UserService.ExternalGetAsync(externalId);
            if (user?.Id != null) return false;
            return true;
        }

        bool CompanyExist(int? companyId)
        {
            var company = _dataService.CompanyService.GetAsync(companyId.GetValueOrDefault());
            if (company?.Id != null) return true;
            return false;
        }
    }
}
