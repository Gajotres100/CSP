using ComProvis.AV.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComProvis.AV.Validators
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
                Must(UserExist).WithMessage(Resource.ErrUserExist);
            RuleFor(x => x.CompanyId).
                NotEmpty().WithMessage(Resource.ErrObjectIsNull).
                Must(CompanyExist).WithMessage(Resource.ErrCustomerDontExist);
            RuleFor(x => x.Email).
               Length(1, 255).WithMessage(Resource.ErrObjectLength255).
               Matches(Resource.RegexEmail).WithMessage(Resource.ErrObjectIsNotValid);
        }

        bool UserExist(string externalId)
        {
            var user = _dataService.UserService.ExternalGet(externalId);
            if (user?.Id != null) return false;
            return true;
        }

        bool CompanyExist(int? companyId)
        {
            var company = _dataService.CompanyService.Get(companyId.GetValueOrDefault());
            if (company?.Id != null) return true;
            return false;
        }
    }
}
